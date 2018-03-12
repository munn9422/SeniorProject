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
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var model = new IndexViewModel();
            model.userClassroomRole = UserManager.FindById(User.Identity.GetUserId()).ClassroomRole;
            return View(model);
        }

        //TODO SANITIZE FORM DATA
        // POST: /Manage/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string role, string fname)
        {
            if (role != null)
            {
                
                if (role == "Student")
                {
                    var student = new Student();
                    student.studentFirst = fname;
                }
                else if (role == "Teacher")
                {
                    var teacher = new Teacher();
                    teacher.teacherFirst = fname;
                }
                else if (role == "Parent")
                {
                    var parent = new Parent();
                    parent.parentFirst = fname;
                }
                //get logged in user from browser context
                ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
                user.ClassroomRole = role;

                UserManager.Update(user);
            }


            var vmodel = new IndexViewModel();
            vmodel.userClassroomRole = role;
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