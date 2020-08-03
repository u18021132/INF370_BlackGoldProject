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
    public class OtherBuildingDetailController : ApiController
    { 
        //READ ALL DATA//    
        [HttpGet]
        [Route("api/otherbuildingdetail")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all other building details
                var otherbuildingdetail = db.OTHERBUILDINGDETAILs.Select(x => new { x.OTHERBUILDINGDETAILID, x.OTHERBUILDINGDETAILDESCRIPTION }).ToList();

                if (otherbuildingdetail == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(otherbuildingdetail);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/otherbuildingdetail")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified other building detail
                var otherbuildingdetail = db.OTHERBUILDINGDETAILs.Where(z => z.OTHERBUILDINGDETAILID == id).Select(x => new { x.OTHERBUILDINGDETAILID, x.OTHERBUILDINGDETAILDESCRIPTION }).FirstOrDefault();

                if (otherbuildingdetail == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(otherbuildingdetail);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //ADD//
        [HttpPost]
        [Route("api/otherbuildingdetail")]
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

                //Add other building detail
                db.OTHERBUILDINGDETAILs.Add(new OTHERBUILDINGDETAIL
                {
                    OTHERBUILDINGDETAILDESCRIPTION = description
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
        [Route("api/otherbuildingdetail")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string description)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var otherbuildingdetails = db.OTHERBUILDINGDETAILs.FirstOrDefault(x => x.OTHERBUILDINGDETAILID == id);

                //Null checks
                if (string.IsNullOrEmpty(description))
                    return BadRequest();

                //Update specified other building detail
                otherbuildingdetails.OTHERBUILDINGDETAILDESCRIPTION = description;

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
        [Route("api/otherbuildingdetail")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;

                //Find other building detail
                var otherbuildingdetail = db.OTHERBUILDINGDETAILs.FirstOrDefault(x => x.OTHERBUILDINGDETAILID == id);
                if (otherbuildingdetail == null)
                    return NotFound();

                //Delete specified other building detail
                db.OTHERBUILDINGDETAILs.Remove(otherbuildingdetail);

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
