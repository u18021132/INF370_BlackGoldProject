//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlackGoldProperties_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MANDATETYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MANDATETYPE()
        {
            this.MANDATEs = new HashSet<MANDATE>();
        }
    
        public int MANDATETYPEID { get; set; }
        public string MANDATETYPEDESCRIPTION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MANDATE> MANDATEs { get; set; }
    }
}
