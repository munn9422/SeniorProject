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
                if(studentUser == null)
                {
                    studentUser = new Student();
                }
                model.userfname = studentUser.studentFirst;
            }
            else if (AspNetUser.ClassroomRole == "Parent")
            {
                Parent parentUser = UserBinding.getParent(AspNetUser.Id);
                //TODO null check
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
                }
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
                    var student = UserBinding.getStudent(User.Identity.GetUserId());
                    if (student != null)
                    {
                        student.studentFirst = fname;
                        UserBinding.SaveStudent(student);
                    }
                    else
                    {
                        var newstudent = new Student
                        {
                            StudentID = UserBinding.GenerateStudentId(),
                            studentFirst = fname,
                            UserID = User.Identity.GetUserId(),
                            studentUsername = User.Identity.GetUserName()
                        };
                        UserBinding.SaveStudent(newstudent);
                    }

                }
                else if (role == "Teacher")
                {
                    var teacher = UserBinding.getTeacher(User.Identity.GetUserId());
                    if (teacher != null)
                    {
                        teacher.teacherFirst = fname;
                        teacher.teacherLast = lname;
                        UserBinding.SaveTeacher(teacher);
                    }
                    else
                    {
                        var newteacher = new Teacher
                        {
                            TeacherID = UserBinding.GenerateTeacherId(),
                            teacherFirst = fname,
                            teacherLast = lname,
                            UserID = User.Identity.GetUserId(),
                            teacherUsername = User.Identity.GetUserName()
                        };
                        UserBinding.SaveTeacher(newteacher);
                    }
                }
                else if (role == "Parent")
                {
                    var parent = new Parent
                    {
                        parentID = UserBinding.GenerateParentId(),
                        parentFirst = fname,
                        UserID = User.Identity.GetUserId(),
                        parentUsername = User.Identity.GetUserName()
                    };
                    UserBinding.SaveParent(parent);
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