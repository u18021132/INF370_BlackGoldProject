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
    public class AgentRentalAgreementsController : ApiController
    {
        //READ ALL DATA//
        [HttpGet]
        [Route("api/agentrentalagreement")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all rental agreements   --- Needs document also
                var rentalagreement = db.RENTALs.Select(x => new {
                    x.RENTALID,
                    x.RENTALDATESTART,
                    x.RENTALDATEEND,
                    x.RENTALSTATU.RENTALSTATUSID,
                    x.RENTALSTATU.RENTALSTATUSDESCRIPTION,
                    x.RENTALAGREEMENTDOCUMENT,
                    x.PROPERTY.PROPERTYID,
                    x.PROPERTY.PROPERTYADDRESS,
                    x.CLIENT.USER.USERID,
                    x.CLIENT.USER.USERNAME,
                    x.CLIENT.USER.USERSURNAME,
                    x.CLIENT.USER.USERCONTACTNUMBER,
                    x.CLIENT.USER.USEREMAIL
                }).ToList();

                if (rentalagreement == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(rentalagreement);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/agentrentalagreement")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified rental agreement
                var rentalagreement = db.RENTALs.Where(z => z.RENTALID == id).Select(x => new {
                    x.RENTALID,
                    x.RENTALDATESTART,
                    x.RENTALDATEEND,
                    x.RENTALSTATU.RENTALSTATUSID,
                    x.RENTALSTATU.RENTALSTATUSDESCRIPTION,
                    x.RENTALAGREEMENTDOCUMENT,
                    x.PROPERTY.PROPERTYID,
                    x.PROPERTY.PROPERTYADDRESS,
                    x.CLIENT.USER.USERID,
                    x.CLIENT.USER.USERNAME,
                    x.CLIENT.USER.USERSURNAME,
                    x.CLIENT.USER.USERCONTACTNUMBER,
                    x.CLIENT.USER.USEREMAIL
                }).FirstOrDefault();

                if (rentalagreement == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(rentalagreement);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
