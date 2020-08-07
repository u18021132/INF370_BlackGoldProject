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
    public class DefectController : ApiController
    {
        //READ ALL DATA//
        [HttpGet]
        [Route("api/defect")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all defects
                var defect = db.DEFECTs.Select(x => new {
                    x.DEFECTID,
                    x.DEFECTDESCRIPTION
                }).ToList();

                if (defect == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(defect);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/defect")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified defect
                var defect = db.DEFECTs.Where(z => z.DEFECTID == id).Select(x => new { 
                    x.DEFECTID, 
                    x.DEFECTDESCRIPTION
                }).FirstOrDefault();

                if (defect == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(defect);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //ADD//
        [HttpPost]
        [Route("api/defect")]
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

                //Add a defect
                db.DEFECTs.Add(new DEFECT
                {
                    DEFECTDESCRIPTION = description
                });

                //Save DB changes
                db.SaveChanges();

                //Return Ok
                return Ok();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //UPDATE//
        [HttpPatch]
        [Route("api/defect")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string description)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var defects = db.DEFECTs.FirstOrDefault(x => x.DEFECTID == id);

                //Null checks
                if (string.IsNullOrEmpty(description))
                    return BadRequest();

                //Update specified defect
                defects.DEFECTDESCRIPTION = description;

                //Save DB changes
                db.SaveChanges();

                //Return Ok
                return Ok();
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }


        //DELETE//
        [HttpDelete]
        [Route("api/defect")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;

                //Find defect
                var defect = db.DEFECTs.FirstOrDefault(x => x.DEFECTID == id);
                if (defect == null)
                    return NotFound();

                //Delete specified defect
                db.DEFECTs.Remove(defect);

                //Save DB Changes
                db.SaveChanges();

                //Return Ok
                return Ok();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
    }
}
