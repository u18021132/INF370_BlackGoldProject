using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._10._Location._10._Suburb
{
    public class SuburbController : ApiController
    {
        //READ ALL DATA//
        [HttpGet]
        [Route("api/suburb")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all suburbs
                var suburb = db.SUBURBs.Select(x => new { x.SUBURBID, x.SUBURBNAME, x.CITY.CITYID, x.CITY.CITYNAME }).ToList();

                if (suburb == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(suburb);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/suburb")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified suburb
                var suburb = db.SUBURBs.Where(z => z.SUBURBID == id).Select(x => new { x.SUBURBID, x.SUBURBNAME, x.CITY.CITYID, x.CITY.CITYNAME }).FirstOrDefault();

                if (suburb == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(suburb);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //ADD//
        [HttpPost]
        [Route("api/suburb")]
        public IHttpActionResult Post([FromUri] string suburbname, [FromUri] int cityid)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Null checks
                if (string.IsNullOrEmpty(suburbname))
                    return BadRequest();

                //Add a suburb
                db.SUBURBs.Add(new SUBURB
                {
                    SUBURBNAME = suburbname,
                    CITYID = cityid
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
        [Route("api/suburb")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string suburbname, [FromUri] int cityid)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var suburbs = db.SUBURBs.FirstOrDefault(x => x.SUBURBID == id);

                //Null checks
                if (string.IsNullOrEmpty(suburbname))
                    return BadRequest();

                //Update specified suburb
                suburbs.SUBURBNAME = suburbname;
                suburbs.CITYID = cityid;

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
        [Route("api/suburb")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;

                //Find suburb
                var suburb = db.SUBURBs.FirstOrDefault(x => x.SUBURBID == id);
                if (suburb == null)
                    return NotFound();

                //Delete specified suburb
                db.SUBURBs.Remove(suburb);

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
