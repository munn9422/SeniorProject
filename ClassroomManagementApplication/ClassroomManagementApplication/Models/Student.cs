//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.BehaviorPerformeds = new HashSet<BehaviorPerformed>();
            this.PrizeRequests = new HashSet<PrizeRequest>();
        }
    
        public decimal StudentID { get; set; }
        public string studentUsername { get; set; }
        public string studentFirst { get; set; }
        public Nullable<decimal> totalPoints { get; set; }
        public Nullable<decimal> parentID { get; set; }
        public Nullable<decimal> classID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BehaviorPerformed> BehaviorPerformeds { get; set; }
        public virtual Classroom Classroom { get; set; }
        public virtual Parent Parent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrizeRequest> PrizeRequests { get; set; }
    }
}