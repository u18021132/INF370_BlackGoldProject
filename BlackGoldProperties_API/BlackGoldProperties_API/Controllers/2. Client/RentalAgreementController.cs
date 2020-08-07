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
    public class RentalAgreementController : ApiController
    {
        //READ ALL DATA//       --- FIX: This should only return clients rental agreements
        [HttpGet]
        [Route("api/rentalagreement")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all rental agreements
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
        [Route("api/rentalagreement")]
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

        //Accept/Reject Rental Agreement//  --signed rental agreement doc to be uploaded
        [HttpPatch]
        [Route("api/rentalagreement")]
        public IHttpActionResult Patch([FromUri] int propertyid, [FromUri] bool accepted, [FromUri] int term, [FromUri] DateTime start)  //--Last 2 parameters to be removed.. see comment below
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var rental = db.RENTALs.FirstOrDefault(x => x.PROPERTYID == propertyid);
                //var application = db.RENTALAPPLICATIONs.FirstOrDefault(x => x.PROPERTYID == propertyid);  //use this to get the new variables to be added to the database being "startdate" and "term".. then these parameters can be removed but rather taken from the renal application

                //Null checks                             --Finish this
                //if (string.IsNullOrEmpty(description))
                //    return BadRequest();

                //Update specified rental
                if(accepted == true)
                {
                    if(term == 1) //6 months
                    {
                        rental.RENTALSTATUSID = 2; //Sets the RentalStatus to 'Rented'
                        rental.RENTALDATESTART = start;
                        rental.RENTALDATEEND = start.AddMonths(6);
                    }

                    else if (term == 2) //12 months
                    {
                        rental.RENTALSTATUSID = 2; //Sets the RentalStatus to 'Rented'
                        rental.RENTALDATESTART = start;
                        rental.RENTALDATEEND = start.AddMonths(12);
                    }

                    else if (term == 3) //18 months
                    {
                        rental.RENTALSTATUSID = 2; //Sets the RentalStatus to 'Rented'
                        rental.RENTALDATESTART = start;
                        rental.RENTALDATEEND = start.AddMonths(18);
                    }

                    else if (term == 4) //24 months
                    {
                        rental.RENTALSTATUSID = 2; //Sets the RentalStatus to 'Rented'
                        rental.RENTALDATESTART = start;
                        rental.RENTALDATEEND = start.AddMonths(24);
                    }
                }    

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


        //Extend/Terminate Rental Agreement//  
        [HttpPost]
        [Route("api/rentalagreement")]

        public IHttpActionResult Post([FromUri] int propertyid, [FromUri] bool terminate, [FromUri] bool extend)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var rental = db.RENTALs.FirstOrDefault(x => x.PROPERTYID == propertyid);


                //Null checks
                //if (string.IsNullOrEmpty(request))
                //return BadRequest();
                if (rental == null)
                    return NotFound();

                    if ( terminate == true)
                    {
                        rental.RENTALSTATUSID = 4; //Sets to 'Pending Termination'
                    }
                
                    if (extend == true)
                    {
                        rental.RENTALSTATUSID = 3; //Sets to 'Pending Extension'
                    }

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
