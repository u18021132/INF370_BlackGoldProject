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
    public class UpdateClientInfoController : ApiController
    {
        //UPDATE//
        [HttpPatch]
        [Route("api/updateclientinfo")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string name, [FromUri] string surname, [FromUri] string contactnumber, [FromUri] string altcontactnumber, [FromUri] string address)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var user = db.USERs.FirstOrDefault(x => x.USERID == id);

                //Null checks
                if (string.IsNullOrEmpty(name))
                    return BadRequest();
                if (string.IsNullOrEmpty(surname))
                    return BadRequest();
                if (string.IsNullOrEmpty(address))
                    return BadRequest();

                //Update specified user information
                user.USERNAME = name;
                user.USERSURNAME = surname;
                user.USERCONTACTNUMBER = contactnumber;
                user.USERALTCONTACTNUMBER = altcontactnumber;
                user.USERADDRESS = address;

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
