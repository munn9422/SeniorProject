using ClassroomManagementApplication.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace ClassroomManagementApplication.Controllers
{
    public class BehaviorController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public BehaviorController()
        {
        }

        public BehaviorController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //// GET: Behavior
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //GET: Behavior/Add
        [Authorize]
        public ActionResult Add(string studentuserID, string classcode)
        {
            if (classcode == null ) {
                //error message
                return RedirectToAction("Classroom", "Index", new {classcode = classcode});
            }
            Classroom cr = ClassroomBinding.GetClassroomFromCode(classcode);
            if(cr == null) {
                //error message
                return RedirectToAction("Classroom", "Index", new { classcode = classcode });
            }

            NewBehaviorViewModel model = new NewBehaviorViewModel() {
                ClassCode = classcode,
                StudentUserID = User.Identity.GetUserId()
            };
            model.BehaviorTypes = new List<SelectListItem>();
            List<BehaviorType> bts = cr.BehaviorType.ToList();
            foreach (BehaviorType bt in bts) {
                model.BehaviorTypes.Add(new SelectListItem { Text = bt.behaviorTitle, Value = bt.behaviorID.ToString()});
            }
            return View(model);
        }

        //POST: Behavior/Add
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(NewBehaviorViewModel model, FormCollection form)
        {
            if (!ModelState.IsValid || model.DatePerformed == null || model.BehaviorTypePerformedID == null) {
                return RedirectToAction("Home", "Index");
            }
            Student student = UserBinding.getStudent(model.StudentUserID);
            if(student.classID == null) {
                return RedirectToAction("Home", "Index");
            }

            BehaviorType bt = ClassroomBinding.GetBehaviorType((decimal)student.classID, Convert.ToDecimal(model.BehaviorTypePerformedID));
            UserBinding.AddBehaviorToStudent(student, bt, model.DatePerformed);
            return RedirectToAction("Index","Classroom", new { classcode = ClassroomBinding.GetClassroomFromID(student.classID).classCode });
        }

        //GET: Behavior/AddType
        [Authorize]
        public ActionResult AddType(string classcode)
        {
            AddTypeViewModel model = new AddTypeViewModel();
            model.ClassCode = classcode;
            return View(model);
        }

        //POST: Behavior/AddType
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddType(AddTypeViewModel type)
        {
            //TODO null check
            var behavior = new BehaviorType
            {
                behaviorID = BehaviorBinding.GenerateBehaviorId(),
                behaviorTitle = type.Title,
                behaviorDescription = type.Description,
                pointValue = type.PointValue,
                classID = ClassroomBinding.GetClassroomFromCode(type.ClassCode).classID
            };

            BehaviorBinding.SaveBehavior(behavior);
            return View();
        }
    }
}