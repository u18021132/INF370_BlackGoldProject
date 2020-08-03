using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers
{
    public class PointOfInterestController : ApiController
    {
        //READ ALL DATA//
        [HttpGet]
        [Route("api/pointofinterest")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all points of interest
                //var selection = db.POINTOFINTERESTs.Select(x => new { x.POINTOFINTERESTID, x.POINTOFINTERESTNAME, x.POINTOFINTERESTTYPE, x.SUBURBPOINTOFINTERESTs }).ToList();
                var pointofinterest = db.SUBURBPOINTOFINTERESTs.Select(x => new {
                    x.POINTOFINTEREST.POINTOFINTERESTID,
                    x.POINTOFINTEREST.POINTOFINTERESTNAME,
                    x.POINTOFINTEREST.POINTOFINTERESTTYPE.POINTOFINTERESTTYPEID,
                    x.POINTOFINTEREST.POINTOFINTERESTTYPE.POINTOFINTERESTTYPEDESCRIPTION,
                    x.SUBURB.SUBURBID,
                    x.SUBURB.SUBURBNAME
                }).ToList();
                if (pointofinterest == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(pointofinterest);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/pointofinterest")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified point of interest
                var pointofinterest = db.SUBURBPOINTOFINTERESTs.Where(z => z.POINTOFINTERESTID == id).Select(x => new {
                    x.POINTOFINTEREST.POINTOFINTERESTID,
                    x.POINTOFINTEREST.POINTOFINTERESTNAME,
                    x.POINTOFINTEREST.POINTOFINTERESTTYPE.POINTOFINTERESTTYPEID,
                    x.POINTOFINTEREST.POINTOFINTERESTTYPE.POINTOFINTERESTTYPEDESCRIPTION,
                    x.SUBURB.SUBURBID,
                    x.SUBURB.SUBURBNAME
                }).FirstOrDefault();
                
                if (pointofinterest == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(pointofinterest);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //ADD//  
        [HttpPost]
        [Route("api/pointofinterest")]
        public IHttpActionResult Post([FromUri] string description, [FromUri] int typeid, [FromUri] int suburbid)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Null checks
                if (string.IsNullOrEmpty(description))
                    return BadRequest();

                //Add a point of interest
                db.POINTOFINTERESTs.Add(new POINTOFINTEREST
                {
                    POINTOFINTERESTNAME = description,
                    POINTOFINTERESTTYPEID = typeid
                    
                });

                //Save DB changes
                db.SaveChanges();

                //Find the point of interest that was just added
                int lastid = db.POINTOFINTERESTs.Max(item => item.POINTOFINTERESTID);

                //Link the new point of interest to a suburb
                db.SUBURBPOINTOFINTERESTs.Add(new SUBURBPOINTOFINTEREST
                {
                    POINTOFINTERESTID = lastid,
                    SUBURBID = suburbid
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
        [Route("api/pointofinterest")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string description, [FromUri] int typeid, [FromUri] int suburbid)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var pointofinterests = db.POINTOFINTERESTs.FirstOrDefault(x => x.POINTOFINTERESTID == id);
                var suburbpointofinterests = db.SUBURBPOINTOFINTERESTs.FirstOrDefault(x => x.POINTOFINTERESTID == id);

                //Null checks
                if (string.IsNullOrEmpty(description))
                    return BadRequest();

                //Update specified point of interest
                pointofinterests.POINTOFINTERESTNAME= description;
                pointofinterests.POINTOFINTERESTTYPEID = typeid;
                suburbpointofinterests.SUBURBID = suburbid;

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


        //DELETE//    ---UNABLE TO DELETE:  BECAUSE OF SUBURBPOINTOFINTEREST ENTITY == CHECK property controller on how to fix this issue
        [HttpDelete]
        [Route("api/pointofinterest")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;

               //Find point of interest
                var pointofinterest = db.POINTOFINTERESTs.FirstOrDefault(x => x.POINTOFINTERESTID == id);
                var suburbpointofinterests = db.SUBURBPOINTOFINTERESTs.FirstOrDefault(x => x.POINTOFINTERESTID == id);
                if (pointofinterest == null)
                    return NotFound();

                //Delete specified point of interest
                db.POINTOFINTERESTs.Remove(pointofinterest);
                
                //Save DB Changes
                db.SaveChanges();

                db.SUBURBPOINTOFINTERESTs.Remove(suburbpointofinterests);

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
