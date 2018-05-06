using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ClassroomManagementApplication.Models
{
    public class NewBehaviorViewModel
    {
        public List<SelectListItem> BehaviorTypes { get; set; }
        public string BehaviorTypePerformedID { get; set; }
        public DateTime DatePerformed { get; set; }
        public string StudentUserID { get; set; }
        public string ClassCode { get; set; }
    }
}