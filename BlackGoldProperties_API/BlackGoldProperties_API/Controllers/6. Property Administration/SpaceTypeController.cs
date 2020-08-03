using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._6._Space_Type
{
    public class SpaceTypeController : ApiController
    {
        //READ ALL DATA//
        [HttpGet]
        [Route("api/spacetype")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all space types
                var spacetype = db.SPACETYPEs.Select(x => new { 
                    x.SPACETYPEID, 
                    x.SPACETYPEDESCRIPTION 
                }).ToList();

                if (spacetype == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(spacetype);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/spacetype")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified space type
                var spacetype = db.SPACETYPEs.Where(z => z.SPACETYPEID == id).Select(x => new { 
                    x.SPACETYPEID, 
                    x.SPACETYPEDESCRIPTION 
                }).FirstOrDefault();

                if (spacetype == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(spacetype);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //ADD//
        [HttpPost]
        [Route("api/spacetype")]
        public IHttpActionResult Post([FromUri] string description)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Null checks
                if (string.IsNullOrEmpty(description))
                    return BadRequest();

                //Add a space type
                db.SPACETYPEs.Add(new SPACETYPE
                {
                    SPACETYPEDESCRIPTION = description
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
        [Route("api/spacetype")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string description)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var spacetypes = db.SPACETYPEs.FirstOrDefault(x => x.SPACETYPEID == id);

                //Null checks
                if (string.IsNullOrEmpty(description))
                    return BadRequest();

                //Update specified space type
                spacetypes.SPACETYPEDESCRIPTION = description;

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
        [Route("api/spacetype")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;

                //Find space type
                var spacetype = db.SPACETYPEs.FirstOrDefault(x => x.SPACETYPEID == id);
                if (spacetype == null)
                    return NotFound();

                //Delete specified space type
                db.SPACETYPEs.Remove(spacetype);

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

