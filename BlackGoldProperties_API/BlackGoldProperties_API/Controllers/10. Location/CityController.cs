using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._10._Location._10._City
{
    public class CityController : ApiController
    {
        //READ ALL DATA//
        [HttpGet]
        [Route("api/city")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all cities
                var city = db.CITies.Select(x => new { 
                    x.CITYID, 
                    x.CITYNAME, 
                    x.PROVINCE.PROVINCEID, 
                    x.PROVINCE.PROVINCENAME
                }).ToList();

                if (city == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(city);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/city")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified city
                var city = db.CITies.Where(z => z.CITYID == id).Select(x => new { x.CITYID, x.CITYNAME, x.PROVINCE.PROVINCEID, x.PROVINCE.PROVINCENAME }).FirstOrDefault();

                if (city == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(city);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //ADD//
        [HttpPost]
        [Route("api/city")]
        public IHttpActionResult Post([FromUri] string cityname, [FromUri] int provinceid)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Null checks
                if (string.IsNullOrEmpty(cityname))
                    return BadRequest();

                //Add a city
                db.CITies.Add(new CITY
                {
                    CITYNAME = cityname,
                    PROVINCEID = provinceid
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
        [Route("api/city")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string cityname, [FromUri] int provinceid)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var cities = db.CITies.FirstOrDefault(x => x.CITYID == id);

                //Null checks
                if (string.IsNullOrEmpty(cityname))
                    return BadRequest();

                //Update specified city
                cities.CITYNAME = cityname;
                cities.PROVINCEID = provinceid;

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
        [Route("api/city")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;

                //Find city
                var city = db.CITies.FirstOrDefault(x => x.CITYID == id);
                if (city == null)
                    return NotFound();

                //Delete specified city
                db.CITies.Remove(city);

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
