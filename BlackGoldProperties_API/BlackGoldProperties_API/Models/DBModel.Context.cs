﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BlackGoldDBEntities10 : DbContext
    {
        public BlackGoldDBEntities10()
            : base("name=BlackGoldDBEntities10")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADVERTISEMENT> ADVERTISEMENTs { get; set; }
        public virtual DbSet<AUDITREPORT> AUDITREPORTs { get; set; }
        public virtual DbSet<CITY> CITies { get; set; }
        public virtual DbSet<CLIENT> CLIENTs { get; set; }
        public virtual DbSet<CLIENTDOCUMENT> CLIENTDOCUMENTs { get; set; }
        public virtual DbSet<CLIENTDOCUMENTTYPE> CLIENTDOCUMENTTYPEs { get; set; }
        public virtual DbSet<CLIENTTYPE> CLIENTTYPEs { get; set; }
        public virtual DbSet<DEFECT> DEFECTs { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEEs { get; set; }
        public virtual DbSet<EMPLOYEEPROPERTY> EMPLOYEEPROPERTies { get; set; }
        public virtual DbSet<EMPLOYEEROLE> EMPLOYEEROLEs { get; set; }
        public virtual DbSet<EMPLOYEETYPE> EMPLOYEETYPEs { get; set; }
        public virtual DbSet<FEATURE> FEATUREs { get; set; }
        public virtual DbSet<INSPECTION> INSPECTIONs { get; set; }
        public virtual DbSet<INSPECTIONTYPE> INSPECTIONTYPEs { get; set; }
        public virtual DbSet<IVSTATU> IVSTATUS { get; set; }
        public virtual DbSet<LISTINGPICTURE> LISTINGPICTUREs { get; set; }
        public virtual DbSet<MANDATE> MANDATEs { get; set; }
        public virtual DbSet<MANDATETYPE> MANDATETYPEs { get; set; }
        public virtual DbSet<MARKETTYPE> MARKETTYPEs { get; set; }
        public virtual DbSet<OTHERBUILDINGDETAIL> OTHERBUILDINGDETAILs { get; set; }
        public virtual DbSet<POINTOFINTEREST> POINTOFINTERESTs { get; set; }
        public virtual DbSet<POINTOFINTERESTTYPE> POINTOFINTERESTTYPEs { get; set; }
        public virtual DbSet<POTENTIALCLIENT> POTENTIALCLIENTs { get; set; }
        public virtual DbSet<PRICE> PRICEs { get; set; }
        public virtual DbSet<PROPERTY> PROPERTies { get; set; }
        public virtual DbSet<PROPERTYDEFECT> PROPERTYDEFECTs { get; set; }
        public virtual DbSet<PROPERTYDOCUMENT> PROPERTYDOCUMENTs { get; set; }
        public virtual DbSet<PROPERTYDOCUMENTTYPE> PROPERTYDOCUMENTTYPEs { get; set; }
        public virtual DbSet<PROPERTYFEATURE> PROPERTYFEATUREs { get; set; }
        public virtual DbSet<PROPERTYMANDATE> PROPERTYMANDATEs { get; set; }
        public virtual DbSet<PROPERTYOTHERBUILDINGDETAIL> PROPERTYOTHERBUILDINGDETAILs { get; set; }
        public virtual DbSet<PROPERTYOWNER> PROPERTYOWNERs { get; set; }
        public virtual DbSet<PROPERTYSPACE> PROPERTYSPACEs { get; set; }
        public virtual DbSet<PROPERTYSTATU> PROPERTYSTATUS { get; set; }
        public virtual DbSet<PROPERTYTERM> PROPERTYTERMs { get; set; }
        public virtual DbSet<PROPERTYTYPE> PROPERTYTYPEs { get; set; }
        public virtual DbSet<PROVINCE> PROVINCEs { get; set; }
        public virtual DbSet<PURCHASEOFFER> PURCHASEOFFERs { get; set; }
        public virtual DbSet<PURCHASEOFFERSTATU> PURCHASEOFFERSTATUS { get; set; }
        public virtual DbSet<RENTAL> RENTALs { get; set; }
        public virtual DbSet<RENTALAPPLICATION> RENTALAPPLICATIONs { get; set; }
        public virtual DbSet<RENTALAPPLICATIONSTATU> RENTALAPPLICATIONSTATUS { get; set; }
        public virtual DbSet<RENTALSTATU> RENTALSTATUS { get; set; }
        public virtual DbSet<RENTALTYPE> RENTALTYPEs { get; set; }
        public virtual DbSet<SALE> SALEs { get; set; }
        public virtual DbSet<SPACE> SPACEs { get; set; }
        public virtual DbSet<SPACETYPE> SPACETYPEs { get; set; }
        public virtual DbSet<SUBURB> SUBURBs { get; set; }
        public virtual DbSet<SUBURBPOINTOFINTEREST> SUBURBPOINTOFINTERESTs { get; set; }
        public virtual DbSet<TERM> TERMs { get; set; }
        public virtual DbSet<USER> USERs { get; set; }
        public virtual DbSet<VALUATION> VALUATIONs { get; set; }
    }
}
