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

    public static class UserBinding
    {
        public static void SaveAccountType(object person)
        {
            if (person == null)
            {
                return;
            }
            if (person.GetType() == typeof(Student))
            {
                Student student = (Student)person;
                using (var context = new Entities())
                {
                    var recordToUpdate = from s in context.Student
                                         where s.StudentID == student.StudentID
                                         select s;
                    List<Student> records = recordToUpdate.ToList();
                    if (recordToUpdate.Count() > 0)
                    {
                        Student old = records.FirstOrDefault();
                        context.Entry(old).CurrentValues.SetValues(student);
                    } else
                    {
                        context.Set<Student>().Add(student);
                    }
                    context.SaveChanges();
                }
            }
            else if (person.GetType() == typeof(Parent))
            {
                Parent parent = (Parent)person;
                using (var context = new Entities())
                {
                    var recordToUpdate = from p in context.Parent
                                         where p.parentID == parent.parentID
                                         select p;
                    List<Parent> records = recordToUpdate.ToList();
                    if (records.Count() > 0)
                    {
                        Parent old = records.FirstOrDefault();
                        context.Entry(old).CurrentValues.SetValues(parent);
                    }
                    else
                    {
                        context.Set<Parent>().Add(parent);
                    }
                    context.SaveChanges();
                }
            }
            else if (person.GetType() == typeof(Teacher))
            {
                Teacher teacher = (Teacher)person;
                using (var context = new Entities())
                {
                    var recordToUpdate = from t in context.Teacher
                                         where t.TeacherID == teacher.TeacherID
                                         select t;
                    List<Teacher> records = recordToUpdate.ToList();
                    if (recordToUpdate.Count() > 0)
                    {
                        Teacher old = records.FirstOrDefault();
                        context.Entry(old).CurrentValues.SetValues(teacher);
                    }
                    else
                    {
                        context.Set<Teacher>().Add(teacher);
                    }
                    context.SaveChanges();
                }
            }
        }
        public static Student getStudent(string UserID)
        {
            if (UserID == null)
            {
                return null;
            }
            Student st = new Student();
            using (var context = new Entities())
            {
                var query = from s in context.Student
                                     where s.UserID == UserID
                                     select s;
                List<Student> records = query.ToList();
                if (records.Count() > 0)
                {
                    st = records.FirstOrDefault();
                }
            }
            return st;
        }
        public static Parent getParent(string UserID)
        {
            if (UserID == null)
            {
                return null;
            }
            Parent pa = new Parent();
            using (var context = new Entities())
            {
                var query = from p in context.Parent
                            where p.UserID == UserID
                            select p;
                List<Parent> records = query.ToList();
                if (records.Count() > 0)
                {
                    pa = records.FirstOrDefault();
                }
            }
            return pa;
        }
        public static Teacher getTeacher(string UserID)
        {
            if (UserID == null)
            {
                return null;
            }
            Teacher te = new Teacher();
            using (var context = new Entities())
            {
                var query = from t in context.Teacher
                            where t.UserID == UserID
                            select t;
                List<Teacher> records = query.ToList();
                if (records.Count() > 0)
                {
                    te = records.FirstOrDefault();
                }
            }
            return te;
        }
    }
}