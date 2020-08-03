using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._2._Client
{
    public class BrowsePropertiesController : ApiController
    {
        //browse properties on the client side

        //READ ALL Properties//
        [HttpGet]
        [Route("api/browseproperties")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all properties -- ADD OTHERBUILDINGDETAIL WHEN DB IS FIXED
                var properties = db.PROPERTies.Select(x => new { x.PROPERTYID, 
                    x.PROPERTYADDRESS, 
                    x.SUBURB, 
                    x.SUBURB.CITY.CITYNAME, 
                    x.SUBURB.CITY.PROVINCE.PROVINCENAME, 
                    x.MARKETTYPE, 
                    PropertyFeatures = x.PROPERTYFEATUREs.Select(y => new { y.FEATURE, y.PROPERTYFEATUREQUANTITY }).ToList(), 
                    x.PROPERTYSTATU, 
                    x.PRICEs,  //--Fix this to read the last price in the DB for that property 
                    x.PROPERTYTYPE, 
                    PropertySpaces = x.PROPERTYSPACEs.Select(y => new { y.SPACE, y.PROPERTYSPACEQUANTITY, y.SPACE.SPACETYPE.SPACETYPEDESCRIPTION }).ToList(), 
                    x.LISTINGPICTUREs, 
                    x.EMPLOYEE.USER, 
                    PropertyPOI = x.SUBURB.SUBURBPOINTOFINTERESTs.Select(y => new { y.POINTOFINTEREST, y.POINTOFINTEREST.POINTOFINTERESTTYPE.POINTOFINTERESTTYPEDESCRIPTION, y.SUBURB.SUBURBID, y.SUBURB.SUBURBNAME }).ToList() 
                }).ToList();

                if (properties == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(properties);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/browseproperties")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified property
                var property = db.PROPERTies.Where(z => z.PROPERTYID == id).Select(x => new { 
                    x.PROPERTYID,
                    x.PROPERTYADDRESS, 
                    x.SUBURB, //  .Include()
                    x.SUBURB.CITY.CITYNAME, 
                    x.SUBURB.CITY.PROVINCE.PROVINCENAME, 
                    x.MARKETTYPE, 
                    PropertyFeatures = x.PROPERTYFEATUREs.Select(y => new { y.FEATURE, y.PROPERTYFEATUREQUANTITY }).ToList(), 
                    x.PROPERTYSTATU, 
                    x.PRICEs, 
                    x.PROPERTYTYPE, 
                    PropertySpaces =  x.PROPERTYSPACEs.Select(y => new { y.SPACE, y.PROPERTYSPACEQUANTITY, y.SPACE.SPACETYPE.SPACETYPEDESCRIPTION}).ToList(), 
                    x.LISTINGPICTUREs, 
                    x.EMPLOYEE.USER, 
                    PropertyPOI = x.SUBURB.SUBURBPOINTOFINTERESTs.Select(y => new { y.POINTOFINTEREST, y.POINTOFINTEREST.POINTOFINTERESTTYPE.POINTOFINTERESTTYPEDESCRIPTION}).ToList()
                }).FirstOrDefault();

                if (property == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(property);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

    }
}
