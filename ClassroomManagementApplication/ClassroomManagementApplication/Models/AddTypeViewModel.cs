using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassroomManagementApplication.Models
{
    public class AddTypeViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PointValue { get; set; }
        public string ClassCode { get; set; }
    }
}