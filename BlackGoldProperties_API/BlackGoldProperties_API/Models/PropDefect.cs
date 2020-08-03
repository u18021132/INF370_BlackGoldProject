using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackGoldProperties_API.Models
{
    public class PropDefect
    {
        public int PROPERTYDEFECTID { get; set; }
        public int DEFECTID { get; set; }
        public int INSPECTIONID { get; set; }
        public int PROPERTYDEFECTQUANTITY { get; set; }
    }
}