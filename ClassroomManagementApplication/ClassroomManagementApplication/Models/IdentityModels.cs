using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Collections.Generic;

namespace ClassroomManagementApplication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.ClassroomRole = "";
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string ClassroomRole { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public static class BehaviorBinding
    {
        public static void SaveBehavior(BehaviorType bt)
        {

            using (var context = new Entities())
            {
                var recordToUpdate = from b in context.BehaviorType
                                     where b.behaviorID == bt.behaviorID
                                     select b;

                List<BehaviorType> records = recordToUpdate.ToList();
                if (recordToUpdate.Count() > 0)
                {
                    BehaviorType old = records.FirstOrDefault();
                    context.Entry(old).CurrentValues.SetValues(bt);
                }
                else
                {
                    context.Set<BehaviorType>().Add(bt);
                }
                context.SaveChanges();
            }
        }

        public static decimal GenerateBehaviorId()
        {
            using (var context = new Entities())
            {
                var query = from b in context.BehaviorType
                            orderby b.behaviorID descending
                            select b;
                List<BehaviorType> behaviors = query.ToList();
                if (behaviors.Count < 1)
                {
                    return 0;
                }
                var largestid = behaviors.First().behaviorID;
                return ++largestid;
            }

        }
    }

}