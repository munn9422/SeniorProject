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
        public ActionResult Add(string title, string desc, decimal point)
        {
            var behavior = new BehaviorType
            {
                behaviorID = BehaviorBinding.GenerateBehaviorId(),
                behaviorTitle = title,
                behaviorDescription = desc,
                pointValue = point
            };

            BehaviorBinding.SaveBehavior(behavior);
            return View();
        }
    }
}