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
            this.BehaviorPerformed = new HashSet<BehaviorPerformed>();
            this.PrizeRequest = new HashSet<PrizeRequest>();
        }
    
        public decimal StudentID { get; set; }
        public string studentUsername { get; set; }
        public string studentFirst { get; set; }
        public Nullable<decimal> totalPoints { get; set; }
        public Nullable<decimal> parentID { get; set; }
        public Nullable<decimal> classID { get; set; }
        public string UserID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BehaviorPerformed> BehaviorPerformed { get; set; }
        public virtual Classroom Classroom { get; set; }
        public virtual Parent Parent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrizeRequest> PrizeRequest { get; set; }
    }
}
