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
    public class PropertyOwnerController : ApiController
    {
        //READ ALL DATA//
        [HttpGet]
        [Route("api/propertyowner")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all property owners
                var propertyowner = db.PROPERTYOWNERs.Select(x => new { 
                    x.PROPERTYOWNERID, 
                    x.PROPERTYOWNERNAME, 
                    x.PROPERTYOWNERSURNAME, 
                    x.PROPERTYOWNEREMAIL, 
                    x.PROPERTYOWNERIDNUMBER, 
                    x.PROPERTYOWNERADDRESS, 
                    x.PROPERTYOWNERCONTACTNUMBER, 
                    x.PROPERTYOWNERALTCONTACTNUMBER
                }).ToList();

                if (propertyowner == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(propertyowner);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/propertyowner")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified property owner
                var propertyowner = db.PROPERTYOWNERs.Where(z => z.PROPERTYOWNERID == id).Select(x => new {
                    x.PROPERTYOWNERID,
                    x.PROPERTYOWNERNAME,
                    x.PROPERTYOWNERSURNAME,
                    x.PROPERTYOWNEREMAIL,
                    x.PROPERTYOWNERIDNUMBER,
                    x.PROPERTYOWNERADDRESS,
                    x.PROPERTYOWNERCONTACTNUMBER,
                    x.PROPERTYOWNERALTCONTACTNUMBER
                }).FirstOrDefault();

                if (propertyowner == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(propertyowner);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        //UPDATE//
        [HttpPatch]
        [Route("api/propertyowner")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string name, [FromUri] string surname, [FromUri] string email, [FromUri] string idnumber, [FromUri] string contactnumber, [FromUri] string altcontactnumber, [FromUri] string address)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var propertyowner = db.PROPERTYOWNERs.FirstOrDefault(x => x.PROPERTYOWNERID == id);

                //Null checks
                if (string.IsNullOrEmpty(name))
                    return BadRequest();

                //Update specified property owner
                propertyowner.PROPERTYOWNERNAME = name;
                propertyowner.PROPERTYOWNERSURNAME = surname;
                propertyowner.PROPERTYOWNEREMAIL = email;
                propertyowner.PROPERTYOWNERIDNUMBER = idnumber;
                propertyowner.PROPERTYOWNERCONTACTNUMBER = contactnumber;
                propertyowner.PROPERTYOWNERALTCONTACTNUMBER = altcontactnumber;
                propertyowner.PROPERTYOWNERADDRESS = address;

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
