using ClassroomManagementApplication.Models;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

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

        // GET: Behavior
        public ActionResult Index()
        {
            return View();
        }

        //GET: Behavior/Add
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        //POST: Behavior/Add
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(string someparameters)
        {
            return View();
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