using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;

namespace ClassroomManagementApplication.Models
{
    public class IndexViewModel :  ExternalLoginConfirmationViewModel
    {
        public string userClassroomRole { get; set; }
        public string userfname { get; set; }
        public string userlname { get; set; }
        public List<Classroom> userClassrooms { get; set; }
    }
}