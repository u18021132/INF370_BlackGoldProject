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
    
    public partial class PROPERTYDOCUMENT
    {
        public int PROPERTYDOCUMENTID { get; set; }
        public int PROPERTYID { get; set; }
        public int PROPERTYDOCUMENTTYPEID { get; set; }
        public byte[] PROPERTYDOCUMENT1 { get; set; }
    
        public virtual PROPERTY PROPERTY { get; set; }
        public virtual PROPERTYDOCUMENTTYPE PROPERTYDOCUMENTTYPE { get; set; }
    }
}
