using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._6._Feature
{
    public class FeatureController : ApiController
    {
        //READ ALL DATA//   ---- DELETE DOESNT WORK
        [HttpGet]
        [Route("api/feature")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all features
                var feature = db.FEATUREs.Select(x => new { x.FEATUREID, x.FEATUREDESCRIPTION }).ToList();

                if (feature == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(feature);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/feature")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified feature
                var feature = db.FEATUREs.Where(z => z.FEATUREID == id).Select(x => new { x.FEATUREID, x.FEATUREDESCRIPTION }).FirstOrDefault();

                if (feature == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(feature);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //ADD//
        [HttpPost]
        [Route("api/feature")]
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

                //Add a feature
                db.FEATUREs.Add(new FEATURE
                {
                    FEATUREDESCRIPTION = description
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
        [Route("api/feature")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string description)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var features = db.FEATUREs.FirstOrDefault(x => x.FEATUREID == id);

                //Null checks
                if (string.IsNullOrEmpty(description))
                    return BadRequest();

                //Update specified feature
                features.FEATUREDESCRIPTION = description;

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
        [Route("api/feature")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            
                //DB context
                var db = LinkToDBController.db;
                var obj = db.FEATUREs.Find(id);
                db.FEATUREs.Remove(obj);
                db.SaveChanges();
                //Find feature
                //var feature = db.FEATUREs.FirstOrDefault(x => x.FEATUREID == id);


                //Delete specified feature
                // db.FEATUREs.Remove(feature);

                //Save DB Changes
                // db.SaveChanges();

                //Return ok
                return Ok();
            
        }

    }
}
