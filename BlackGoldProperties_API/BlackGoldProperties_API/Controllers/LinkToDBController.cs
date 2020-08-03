using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;

namespace BlackGoldProperties_API.Controllers
{
    public class LinkToDBController : ApiController
    {
        public static BlackGoldDBEntities5 db = new BlackGoldDBEntities5();
    }
}
