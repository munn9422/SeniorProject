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
    
    public partial class Prize
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prize()
        {
            this.PrizeRequest = new HashSet<PrizeRequest>();
        }
    
        public decimal prizeID { get; set; }
        public string prizeName { get; set; }
        public Nullable<decimal> prizeCost { get; set; }
        public string prizeDescription { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrizeRequest> PrizeRequest { get; set; }
    }
}
