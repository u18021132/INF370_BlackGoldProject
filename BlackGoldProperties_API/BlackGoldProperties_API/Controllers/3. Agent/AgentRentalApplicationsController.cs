using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._3._Agent
{
    public class AgentRentalApplicationsController : ApiController
    {
        //READ ALL DATA//   -- Should this only return ones that are pending? or should we have sections on the front end categorizing them according to their status
        [HttpGet]
        [Route("api/agentrentalapplication")]
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
        [Route("api/agentrentalapplication")]
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


        //Accept/Reject Rental Application//   -- This should create a rental agreement record for the client
        [HttpPatch]
        [Route("api/agentrentalapplication")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] bool accepted)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var rentalapplication = db.RENTALAPPLICATIONs.FirstOrDefault(x => x.RENTALAPPLICATIONID == id);

                //Null checks
                //if (string.IsNullOrEmpty(description))
                //return BadRequest();

                //Accept specified rental application
                if (accepted == true)
                    rentalapplication.RENTALAPPLICATIONSTATUSID = 2; //Sets to 'Approved'

                //Reject specified rental application
                else if (accepted == false)
                    rentalapplication.RENTALAPPLICATIONSTATUSID = 3; //Sets to 'Rejected'

                //Save DB changes
                db.SaveChanges();

                //Return Ok
                return Ok();
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}
