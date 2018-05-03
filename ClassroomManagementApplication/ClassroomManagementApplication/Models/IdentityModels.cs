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
        public static void SaveStudent(Student student)
        {
            if (student == null)
            {
                return;
            }
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
                }
                else
                {
                    context.Set<Student>().Add(student);
                }
                context.SaveChanges();
            }
        }
        public static void SaveParent(Parent parent)
        {
            if (parent == null)
            {
                return;
            }
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
        public static void SaveTeacher(Teacher teacher)
        {
            if (teacher == null)
            {
                return;
            }
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
                    st = records.First();
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
            using (var context = new Entities())
            {
                var query = from t in context.Teacher
                            where t.UserID == UserID
                            select t;
                List<Teacher> records = query.ToList();
                if (records.Count() > 0)
                {
                    return records.First();
                }
            }
            return null;
        }
        public static List<Classroom> getTeacherClassrooms(decimal teacherID)
        {
            if (teacherID.Equals(0.0))
            {
                return new List<Classroom>();
            }
            using (var context = new Entities())
            {
                var query = from c in context.Classroom
                            where c.teacherID == teacherID
                            select c;
                var results = query.ToList();
                if (query.Count() < 1)
                {
                    return new List<Models.Classroom>();
                }
                else return results;
            }
        }

        public static decimal GenerateTeacherId()
        {
            using (var context = new Entities())
            {
                var query = from t in context.Teacher
                            orderby t.TeacherID descending
                            select t;
                List<Teacher> teachers = query.ToList();
                if (teachers.Count < 1)
                {
                    return 0;
                }
                var largestid = teachers.First().TeacherID;
                return ++largestid;
            }
        }

        public static decimal GenerateParentId()
        {
            using (var context = new Entities())
            {
                var query = from p in context.Parent
                            orderby p.parentID descending
                            select p;
                List<Parent> parents = query.ToList();
                if (parents.Count < 1)
                {
                    return 0;
                }
                var largestid = parents.First().parentID;
                return ++largestid;
            }
        }
        public static decimal GenerateStudentId()
        {
            using (var context = new Entities())
            {
                var query = from s in context.Student
                            orderby s.StudentID descending
                            select s;
                List<Student> students = query.ToList();
                if (students.Count < 1)
                {
                    return 0;
                }
                var largestid = students.First().StudentID;
                return ++largestid;
            }
        }
        public static void JoinClass(string studentUserID, string classCode)
        {

            using (var context = new Entities())
            {
                var studentquery = from s in context.Student
                                   where s.UserID == studentUserID
                                   select s;
                var classquery = from cr in context.Classroom
                                 where cr.classCode == classCode
                                 select cr;
                Student stu = studentquery.First();
                Classroom clroom = classquery.First();
                stu.Classroom = clroom;
                context.SaveChanges();
            }
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