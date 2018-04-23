using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassroomManagementApplication.Controllers
{
    public class ClassroomController : Controller
    {
        // GET: Classroom
        public ActionResult Index()
        {
            return View();
        }

        // GET: Classroom/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: /Manage/Index
        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public void Add()
        //{

        //}
    }
}