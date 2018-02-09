using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClassroomManagementApplication.Startup))]
namespace ClassroomManagementApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
