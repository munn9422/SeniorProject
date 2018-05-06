using ClassroomManagementApplication.Models;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace ClassroomManagementApplication.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DateTime date)
        {
            Student s = UserBinding.getStudent(User.Identity.GetUserId());
            UserBinding.RequestPrize(s.StudentID, date);
            return View();
        }
    }
}