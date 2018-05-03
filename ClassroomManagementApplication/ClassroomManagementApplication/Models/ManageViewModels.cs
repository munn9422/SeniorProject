using System.Collections.Generic;

namespace ClassroomManagementApplication.Models
{
    public class IndexViewModel

    {
        public string userClassroomRole { get; set; }
        public string userfname { get; set; }
        public string userlname { get; set; }
        public List<Classroom> userClassrooms { get; set; }
    }
}