using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassroomManagementApplication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ClassroomManagementApplication.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController()
        {

        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var AspNetUser = UserManager.FindById(User.Identity.GetUserId());
                IndexViewModel model = new IndexViewModel
                {
                    userClassroomRole = AspNetUser.ClassroomRole
                };
                if (AspNetUser.ClassroomRole == "Student")
                {
                    Student studentUser = UserBinding.getStudent(AspNetUser.Id);
                    model.userfname = studentUser.studentFirst;
                    model.userClassrooms = new List<Classroom>();
                    model.userClassrooms.Add(studentUser.Classroom);
                }
                else if (AspNetUser.ClassroomRole == "Parent")
                {
                    Parent parentUser = UserBinding.getParent(AspNetUser.Id);
                    model.userfname = parentUser.parentFirst;
                }
                else if (AspNetUser.ClassroomRole == "Teacher")
                {
                    Teacher teacherUser = UserBinding.getTeacher(AspNetUser.Id);
                    if (teacherUser == null)
                    {
                        //TODO add log?
                        return View(new IndexViewModel());
                    }
                    else
                    {
                        model.userfname = teacherUser.teacherFirst;
                        model.userlname = teacherUser.teacherLast;
                        model.userClassrooms = UserBinding.getTeacherClassrooms(teacherUser.TeacherID);
                    }
                    
                }
                return View(model);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}