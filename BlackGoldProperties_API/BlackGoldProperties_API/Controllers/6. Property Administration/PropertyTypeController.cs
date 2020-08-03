using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._6._Property_Administration
{
    public class PropertyTypeController : ApiController
    {
        //READ ALL DATA//
        [HttpGet]
        [Route("api/propertytype")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all property types
                var propertytype = db.PROPERTYTYPEs.Select(x => new { 
                    x.PROPERTYTYPEID, 
                    x.PROPERTYTYPEDESCRIPTION 
                }).ToList();

                if (propertytype == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(propertytype);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/propertytype")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified property type
                var propertytype = db.PROPERTYTYPEs.Where(z => z.PROPERTYTYPEID == id).Select(x => new { 
                    x.PROPERTYTYPEID, 
                    x.PROPERTYTYPEDESCRIPTION 
                }).FirstOrDefault();

                if (propertytype == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(propertytype);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //ADD//
        [HttpPost]
        [Route("api/propertytype")]
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

                //Add a property type
                db.PROPERTYTYPEs.Add(new PROPERTYTYPE
                {
                    PROPERTYTYPEDESCRIPTION = description
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
        [Route("api/propertytype")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string description)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var propertytypes = db.PROPERTYTYPEs.FirstOrDefault(x => x.PROPERTYTYPEID == id);

                //Null checks
                if (string.IsNullOrEmpty(description))
                    return BadRequest();

                //Update specified property type
                propertytypes.PROPERTYTYPEDESCRIPTION = description;

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
        [Route("api/propertytype")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;

                //Find property type
                var propertytype = db.PROPERTYTYPEs.FirstOrDefault(x => x.PROPERTYTYPEID == id);
                if (propertytype == null)
                    return NotFound();

                //Delete specified property type
                db.PROPERTYTYPEs.Remove(propertytype);

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
