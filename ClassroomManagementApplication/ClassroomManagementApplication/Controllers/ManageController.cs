using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ClassroomManagementApplication.Models;

namespace ClassroomManagementApplication.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        //
        // GET: /Manage/Index
        [Authorize]
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.Error ? "An error has occurred." : "";

            var AspNetUser = UserManager.FindById(User.Identity.GetUserId());
            var model = new IndexViewModel
            {
                userClassroomRole = AspNetUser.ClassroomRole
            };
            if (AspNetUser.ClassroomRole == "Student")
            {
                Student studentUser = UserBinding.getStudent(AspNetUser.Id);
                model.userfname = studentUser.studentFirst;
            }
            else if (AspNetUser.ClassroomRole == "Parent")
            {
                Parent parentUser = UserBinding.getParent(AspNetUser.Id);
                model.userfname = parentUser.parentFirst;
            }
            else if (AspNetUser.ClassroomRole == "Teacher")
            {
                Teacher teacherUser = UserBinding.getTeacher(AspNetUser.Id);
                model.userfname = teacherUser.teacherFirst;
                model.userlname = teacherUser.teacherLast;
            }
            return View(model);
        }

        //TODO SANITIZE FORM DATA
        // POST: /Manage/Index
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string role, string fname, string lname)
        {
            if (role != null)
            {
                if (role == "Student")
                {
                    var student = new Student
                    {
                        studentFirst = fname,
                        UserID = User.Identity.GetUserId(),
                        studentUsername = User.Identity.GetUserName()
                    };
                    UserBinding.SaveAccountType(student);
                }
                else if (role == "Teacher")
                {
                    var teacher = new Teacher
                    {
                        TeacherID = UserBinding.GenerateTeacherId(),
                        teacherFirst = fname,
                        teacherLast = lname,
                        UserID = User.Identity.GetUserId(),
                        teacherUsername = User.Identity.GetUserName()
                    };
                    UserBinding.SaveAccountType(teacher);
                }
                else if (role == "Parent")
                {
                    var parent = new Parent
                    {
                        parentFirst = fname,
                        UserID = User.Identity.GetUserId(),
                        parentUsername = User.Identity.GetUserName()
                };
                    UserBinding.SaveAccountType(parent);
                }
                //get logged in user from browser context
                ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
                user.ClassroomRole = role;
                UserManager.Update(user);
            }


            var vmodel = new IndexViewModel
            {
                userClassroomRole = role
            };
            return View(vmodel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            Error
        }

        #endregion
    }
}