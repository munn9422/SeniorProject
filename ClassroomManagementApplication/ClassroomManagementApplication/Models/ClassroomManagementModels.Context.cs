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
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BehaviorPerformed> BehaviorPerformed { get; set; }
        public virtual DbSet<BehaviorType> BehaviorType { get; set; }
        public virtual DbSet<Classroom> Classroom { get; set; }
        public virtual DbSet<Parent> Parent { get; set; }
        public virtual DbSet<Prize> Prize { get; set; }
        public virtual DbSet<PrizeRequest> PrizeRequest { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
    }
}
