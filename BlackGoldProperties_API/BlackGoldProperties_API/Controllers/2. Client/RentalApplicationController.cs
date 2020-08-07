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
    public class RentalApplicationController : ApiController
    {
        //READ ALL DATA//    ---FIX THIS TO READ FOR A SPECIFIC CLIENT
        [HttpGet]
        [Route("api/rentalapplication")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all rental applications
                var rentalapplication = db.RENTALAPPLICATIONs.Select(x => new { 
                    x.RENTALAPPLICATIONID,
                    x.PROPERTY.PROPERTYID, 
                    x.PROPERTY.PROPERTYADDRESS, 
                    x.RENTALAPPLICATIONDATE, 
                    x.RENTALAPPLICATIONSTATU.RENTALAPPLICATIONSTATUSID, 
                    x.RENTALAPPLICATIONSTATU.RENTALAPPLICATIONSTATUSDESCRIPTION, 
                    x.RENTALAPPLICATIONDOCUMENT, 
                    x.CLIENT.USER.USERID, 
                    x.CLIENT.USER.USERNAME, 
                    x.CLIENT.USER.USERSURNAME, 
                    x.CLIENT.USER.USERCONTACTNUMBER, 
                    x.CLIENT.USER.USEREMAIL 
                }).ToList();

                if (rentalapplication == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(rentalapplication);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID// 
        [HttpGet]
        [Route("api/rentalapplication")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified rental application
                var rentalapplication = db.RENTALAPPLICATIONs.Where(z => z.RENTALAPPLICATIONID == id).Select(x => new {
                    x.RENTALAPPLICATIONID,
                    x.PROPERTY.PROPERTYID,
                    x.PROPERTY.PROPERTYADDRESS,
                    x.RENTALAPPLICATIONDATE,
                    x.RENTALAPPLICATIONSTATU.RENTALAPPLICATIONSTATUSID,
                    x.RENTALAPPLICATIONSTATU.RENTALAPPLICATIONSTATUSDESCRIPTION,
                    x.RENTALAPPLICATIONDOCUMENT,
                    x.CLIENT.USER.USERID,
                    x.CLIENT.USER.USERNAME,
                    x.CLIENT.USER.USERSURNAME,
                    x.CLIENT.USER.USERCONTACTNUMBER,
                    x.CLIENT.USER.USEREMAIL
                }).FirstOrDefault();

                if (rentalapplication == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(rentalapplication);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //Apply To Rent//   --This is purely documents and should be done when we fix db file tables   --- Cater for term and start date chosen by user! Cannot make use of paramers yet as DB must be fixed!
        [HttpPost]
        [Route("api/rentalapplication")]
        public IHttpActionResult Post([FromUri] string description, [FromUri] int term, [FromUri] DateTime start)  //-- make rental application status = 1
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Null checks
                if (string.IsNullOrEmpty(description))
                    return BadRequest();

                ////Add a rental application
                //db.DEFECTs.Add(new DEFECT
                //{
                //    DEFECTDESCRIPTION = description
                //});

                //Save DB changes
                db.SaveChanges();

                //Return ok
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
