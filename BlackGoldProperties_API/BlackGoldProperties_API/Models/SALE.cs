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
    
    public partial class SALE
    {
        public int SALEID { get; set; }
        public int PROPERTYID { get; set; }
        public System.DateTime SALEDATECONCLUDED { get; set; }
        public decimal SALEAMOUNT { get; set; }
        public byte[] SALEAGREEMENTDOCUMENT { get; set; }
    
        public virtual PROPERTY PROPERTY { get; set; }
    }
}
