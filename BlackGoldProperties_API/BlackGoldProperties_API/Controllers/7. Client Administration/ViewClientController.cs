using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._7._Client_Administration
{
    public class ViewClientController : ApiController
    {
        //READ ALL DATA//    --- Figure out how to return 2 objects, as theres many tables involved here that all link up.. maybe only one object should be returned, [find the link]
        [HttpGet]
        [Route("api/viewclient")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all clients
                var clients = db.CLIENTs.Select(x => new { x.USERID, x.USER.USERNAME, x.USER.USERSURNAME, x.USER.USERCONTACTNUMBER, x.USER.USEREMAIL, x.USER.USERADDRESS, x.CLIENTTYPEID, x.CLIENTTYPE.CLIENTTYPEDESCRIPTION }).ToList();
                var clientsdocs = db.CLIENTDOCUMENTs.Select(x => new {x.CLIENTDOCUMENTTYPE.CLIENTDOCUMENTTYPEID, x.CLIENTDOCUMENTTYPE.CLIENTDOCUMENTTYPEDESCRIPTION, x.CLIENTDOCUMENT1, x.CLIENTDOCUMENTUPLOADDATE, x.CLIENTDOCUMENTUPLOADEXPIRY}).ToList();

                if (clients == null)
                {
                    return BadRequest();
                }
                if (clientsdocs == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(clients);   //how to return both objects??
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/viewclient")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified client
                var client = db.CLIENTs.Where(z => z.USERID == id).Select(x => new { x.USERID, x.USER.USERNAME, x.USER.USERSURNAME, x.USER.USERCONTACTNUMBER, x.USER.USEREMAIL, x.USER.USERADDRESS, x.CLIENTTYPEID, x.CLIENTTYPE.CLIENTTYPEDESCRIPTION }).FirstOrDefault();

                if (client == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(client);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


    }
}
