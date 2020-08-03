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
    public class ContactAgentController : ApiController
    {
        //Contact agent - [Potential client]   --Test when UserID is no longer not null in PotentialClient table 
        [HttpPost]
        [Route("api/contactagent")]
        public IHttpActionResult Post([FromUri] string email, [FromUri] string name, [FromUri] string surname, [FromUri] string subject, [FromUri] string message, [FromUri] string contactnumber, [FromUri] int userid)
        {
            
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Null checks
                if (string.IsNullOrEmpty(email))
                    return BadRequest();
                if (string.IsNullOrEmpty(name))
                    return BadRequest();
                if (string.IsNullOrEmpty(surname))
                    return BadRequest();
                if (string.IsNullOrEmpty(subject))
                    return BadRequest();
                if (string.IsNullOrEmpty(message))
                    return BadRequest();
                if (string.IsNullOrEmpty(contactnumber))
                    return BadRequest();
                 
            //Check if client already exists as a potential client    --- TEST THIS
            var potential = db.POTENTIALCLIENTs.FirstOrDefault(x => x.POTENTIALCLIENTEMAIL == email);
            if (potential != null)
            {
                //Delete specified client
                db.POTENTIALCLIENTs.Remove(potential);

                //Save DB Changes
                db.SaveChanges();
            }



            //Add a potential client 
            db.POTENTIALCLIENTs.Add(new POTENTIALCLIENT
                {
                    POTENTIALCLIENTEMAIL = email,
                    POTENTIALCLIENTNAME = name,
                    POTENTIALCLIENTSURNAME = surname,
                    POTENTIALCLIENTSUBJECT = subject,
                    POTENTIALCLIENTMESSAGE = message,
                    POTENTIALCLIENTCONTACTNUMBER = contactnumber,
                    USERID = userid
                });

                //Save DB changes
                db.SaveChanges();

                //Return ok
                return Ok();
            
           
        }

    }
}
