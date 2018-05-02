using ClassroomManagementApplication.Models;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace ClassroomManagementApplication.Controllers
{
    public class ClassroomController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ClassroomController()
        {
        }

        public ClassroomController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: Classroom
        [Authorize]
        public ActionResult Index(string classcode)
        {
            Teacher t = UserBinding.getTeacher(User.Identity.GetUserId());

            return View();
        }

        // GET: Classroom/Join
        [Authorize]
        public ActionResult Join()
        {
            return View();
        }

        // GET: Classroom/Join
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join(string parameterhere)
        {
            return View();
        }

        // GET: Classroom/Add
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        //POST: Classroom/Add
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(DateTime start, DateTime end, string classcode)
        {
            Teacher t = UserBinding.getTeacher(User.Identity.GetUserId());
            //TODO clean parameters
            if(t == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ClassroomBinding.getClassroom(classcode)?.classID != null)
            {
                ViewBag.ErrorMessage = "Error: please enter a unique class code.";
                return View();
            }
            var classroom = new Classroom
            {
                classID = ClassroomBinding.GenerateClassId(),
                semesterStart = start,
                semesterEnd = end,
                classCode = classcode,
                teacherID = t.TeacherID
            };
            ClassroomBinding.SaveRoom(classroom);
            return RedirectToAction("Index","Home");
        }
    }
}