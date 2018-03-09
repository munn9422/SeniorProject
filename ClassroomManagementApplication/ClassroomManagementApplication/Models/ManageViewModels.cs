using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;

namespace ClassroomManagementApplication.Models
{
    //Needs to inheret from login view model because the _layout
    //uses the _NavBar which uses the _LoginPartial requiring methods
    //from the LoginViewModel
    public class IndexViewModel : LoginViewModel
    {
        public string userClassroomRole { get; set; }
        //public bool HasPassword { get; set; }
        //public IList<UserLoginInfo> Logins { get; set; }
        //public string PhoneNumber { get; set; }
        //public bool TwoFactor { get; set; }
        //public bool BrowserRemembered { get; set; }
    }
}