using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassroomManagementApplication.Models
{
    public class NewBehaviorViewModel
    {
        public List<BehaviorType> BehaviorTypeList { get; set; }
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
        public Classroom Classroom { get; set; }
    }
}