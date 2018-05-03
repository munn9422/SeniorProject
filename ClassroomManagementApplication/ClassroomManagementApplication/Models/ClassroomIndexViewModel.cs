using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassroomManagementApplication.Models {
    public class ClassroomIndexViewModel {
        public List<Student> Students { get; set; }
        public Teacher Teacher { get; set; }
        public Classroom Classroom { get; set; }
    }
}