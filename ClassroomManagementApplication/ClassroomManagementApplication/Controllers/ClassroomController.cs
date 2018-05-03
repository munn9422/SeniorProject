using ClassroomManagementApplication.Models;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace ClassroomManagementApplication.Controllers {
    public class ClassroomController : Controller {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ClassroomController() {
        }

        public ClassroomController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        // GET: Classroom?classcode={classcode}
        [Authorize]
        public ActionResult Index(string classcode) {
            Teacher t = UserBinding.getTeacher(User.Identity.GetUserId());
            Classroom cr = ClassroomBinding.GetClassroomFromCode(classcode);
            if(cr == null || t == null || classcode == null)
            {
                //TODO add viewbag error?
                return RedirectToAction("Index", "Home");
            }
            ClassroomIndexViewModel model = new ClassroomIndexViewModel();
            model.Classroom = cr;
            model.Teacher = t;
            model.Students = cr.Student.ToList();

            return View(model);
        }

        // GET: Classroom/Join
        [Authorize]
        public ActionResult Join() {


            return View();
        }

        // POST: Classroom/Join
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join(string classCode) {
            var user = User.Identity.GetUserId();
            Classroom cr = ClassroomBinding.GetClassroomFromCode(classCode);
            if(user == null || cr == null || classCode == null)
            {
                ViewBag.ErrorMessage = "Error: There was an issue joining a classroom.";
                return View();
            }
            UserBinding.JoinClass(user, classCode);
            return View();
        }

        // GET: Classroom/Add
        [Authorize]
        public ActionResult Add() {
            return View();
        }

        //POST: Classroom/Add
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(DateTime start, DateTime end, string classcode) {
            Teacher t = UserBinding.getTeacher(User.Identity.GetUserId());
            //TODO clean parameters
            if (t == null) {
                return RedirectToAction("Index", "Home");
            }
            if (ClassroomBinding.GetClassroomFromCode(classcode)?.classID != null) {
                ViewBag.ErrorMessage = "Error: please enter a unique class code.";
                return View();
            }
            var classroom = new Classroom {
                classID = ClassroomBinding.GenerateClassId(),
                semesterStart = start,
                semesterEnd = end,
                classCode = classcode,
                teacherID = t.TeacherID
            };
            ClassroomBinding.SaveRoom(classroom);
            return RedirectToAction("Index", "Home");
        }
    }


}