using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;

namespace ClassroomManagementApplication.Models
{
    //Needs to inherit from login view model because the _layout
    //uses the _NavBar which uses the _LoginPartial requiring methods
    //from the LoginViewModel
    public class IndexViewModel
    {
        public string userClassroomRole { get; set; }
    }
}