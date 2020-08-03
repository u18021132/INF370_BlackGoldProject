using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._6._Space_
{
    public class SpaceController : ApiController
    {
        //READ ALL DATA//
        [HttpGet]
        [Route("api/space")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all spaces
                var space = db.SPACEs.Select(x => new { 
                    x.SPACEID, 
                    x.SPACEDESCRIPTION, 
                    x.SPACETYPE.SPACETYPEID, 
                    x.SPACETYPE.SPACETYPEDESCRIPTION 
                }).ToList();

                if (space == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(space);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//  
        [HttpGet]
        [Route("api/space")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified space
                var space = db.SPACEs.Where(z => z.SPACEID == id).Select(x => new { 
                    x.SPACEID, 
                    x.SPACEDESCRIPTION, 
                    x.SPACETYPE.SPACETYPEID, 
                    x.SPACETYPE.SPACETYPEDESCRIPTION 
                }).FirstOrDefault();

                if (space == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(space);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        //ADD// 
        [HttpPost]
        [Route("api/space")]
        public IHttpActionResult Post([FromUri] string description, [FromUri] int spacetypeid)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Null checks
                if (string.IsNullOrEmpty(description))
                    return BadRequest();

                //Add a space
                db.SPACEs.Add(new SPACE
                {
                    SPACEDESCRIPTION = description,
                    SPACETYPEID = spacetypeid 
                });

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


        //UPDATE//
        [HttpPatch]
        [Route("api/space")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string description, [FromUri] int spacetypeid)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var spaces = db.SPACEs.FirstOrDefault(x => x.SPACEID == id);

                //Null checks
                if (string.IsNullOrEmpty(description))
                    return BadRequest();

                //Update specified space
                spaces.SPACEDESCRIPTION = description;
                spaces.SPACETYPEID = spacetypeid;   

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


        //DELETE//
        [HttpDelete]
        [Route("api/space")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;

                //Find space
                var space = db.SPACEs.FirstOrDefault(x => x.SPACEID == id);
                if (space == null)
                    return NotFound();

                //Delete specified space
                db.SPACEs.Remove(space);

                //Save DB Changes
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
