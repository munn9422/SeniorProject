using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClassroomManagementApplication.Models
{
    public static class ClassroomBinding
    {
        public static void SaveRoom(Classroom cl)
        {

            using (var context = new Entities())
            {
                var recordToUpdate = from c in context.Classroom
                                     where c.classCode == cl.classCode
                                     select c;

                List<Classroom> records = recordToUpdate.ToList();
                if (recordToUpdate.Count() > 0)
                {
                    Classroom old = records.FirstOrDefault();
                    context.Entry(old).CurrentValues.SetValues(cl);
                }
                else
                {
                    context.Set<Classroom>().Add(cl);
                }
                context.SaveChanges();
            }
        }
        public static decimal GenerateClassId()
        {
            using (var context = new Entities())
            {
                var query = from c in context.Classroom
                            orderby c.classID descending
                            select c;
                List<Classroom> classrooms = query.ToList();
                if (classrooms.Count < 1)
                {
                    return 0;
                }
                var largestid = classrooms.First().classID;
                return ++largestid;
            }

        }

        public static Classroom GetClassroomFromCode(string classcode)
        {
            if(classcode == null)
            {
                return null;
            }
            using (var context = new Entities())
            {
                var query = (from c in context.Classroom
                             where c.classCode == classcode
                             select c).Include("Student").Include("Teacher").Include("BehaviorType");
                var results = query.ToList();
                if (results.Count < 1)
                {
                    return null;
                }
                return results.First();
            }
        }
        public static Classroom GetClassroomFromID(decimal? classID)
        {
            if (classID == null)
            {
                return null;
            }
            using (var context = new Entities())
            {
                var query = (from c in context.Classroom
                             where c.classID == classID
                             select c).Include("Student").Include("Teacher").Include("BehaviorType");
                var results = query.ToList();
                if (results.Count < 1)
                {
                    return null;
                }
                return results.First();
            }
        }
    }
}