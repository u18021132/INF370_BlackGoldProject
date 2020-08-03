using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._5._Valuation_Administration
{
    public class ValuationController : ApiController
    {
        //READ ALL DATA//  
        [HttpGet]
        [Route("api/valuation")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all valuations
                var valuations = db.VALUATIONs.Select(x => new { 
                    x.VALUATIONID, 
                    x.VALUATIONDATE, 
                    x.VALUATIONAMOUNT, 
                    x.VALUATIONDOCUMENT, 
                    x.VALUATIONDESCRIPTION, 
                    x.PROPERTY.PROPERTYID, 
                    x.PROPERTY.PROPERTYADDRESS, 
                    x.EMPLOYEE.USER.USERID, 
                    x.EMPLOYEE.USER.USERNAME, 
                    x.EMPLOYEE.USER.USERSURNAME, 
                    x.IVSTATU.IVSTATUSID, 
                    x.IVSTATU.IVSTATUSDESCRIPTION 
                }).ToList();


                if (valuations == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(valuations);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/valuation")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified valuation
                var valuation = db.VALUATIONs.Where(z => z.VALUATIONID == id).Select(x => new { 
                    x.VALUATIONID, 
                    x.VALUATIONDATE, 
                    x.VALUATIONAMOUNT, 
                    x.VALUATIONDOCUMENT, 
                    x.VALUATIONDESCRIPTION, 
                    x.PROPERTY.PROPERTYID, 
                    x.PROPERTY.PROPERTYADDRESS, 
                    x.EMPLOYEE.USER.USERID, 
                    x.EMPLOYEE.USER.USERNAME, 
                    x.EMPLOYEE.USER.USERSURNAME, 
                    x.IVSTATU.IVSTATUSID, 
                    x.IVSTATU.IVSTATUSDESCRIPTION 
                }).FirstOrDefault();

                if (valuation == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(valuation);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //CAPTURE VALUATION//
        [HttpPatch]
        [Route("api/valuation")]   //---- Figure out how to send pdf.. -- FIX USER ID update
        public IHttpActionResult Patch([FromUri] int id , [FromUri] DateTime date, [FromUri] string description, [FromUri] int userid, [FromUri] int IVid/*, [FromUri] byte[] doc*/)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var valuation = db.VALUATIONs.FirstOrDefault(x => x.VALUATIONID == id);

                //Null checks
                if (string.IsNullOrEmpty(description))
                    return BadRequest();

                //Capture valuation 
                valuation.VALUATIONDESCRIPTION = description;
                valuation.VALUATIONDATE = date; //test with - 2017-08-09 format
                valuation.USERID = userid;  
                valuation.IVSTATUSID = IVid;
                    //valuation.VALUATIONDOCUMENT = doc;



                //Save DB changes
                db.SaveChanges();

                //Return ok
                return Ok();
        }
            catch (System.Exception)
            {
                return NotFound();
            }
}
    }
}
