using ClassroomManagementApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassroomManagementApplication.Models;

namespace ClassroomManagementApplication.Controllers
{
    public class ClassroomController : Controller
    {
        public ClassroomController()
        {

        }
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

        //POST: Classroom/Add
    //   [HttpPost]
    //   [Authorize]
    //   [ValidateAntiForgeryToken]
    //    public ActionResult Add(DateTime start, DateTime end, string code)
    //    {
    //        var classroom = new Classroom
    //        {
    //            semesterStart = start,
    //            semesterEnd = end,
    //            classCode = code
    //        };
    //        ClassroomBinding.SaveRoom(classroom);
    //    }
    //}
}