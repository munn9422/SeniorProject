﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassroomManagementApplication.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ClassroomManagementTestEntities : DbContext
    {
        public ClassroomManagementTestEntities()
            : base("name=ClassroomManagementTestEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BehaviorPerformed> BehaviorPerformeds { get; set; }
        public virtual DbSet<BehaviorType> BehaviorTypes { get; set; }
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<Prize> Prizes { get; set; }
        public virtual DbSet<PrizeRequest> PrizeRequests { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
    }
}