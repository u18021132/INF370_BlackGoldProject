using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._10._Location._10._Province
{
    public class ProvinceController : ApiController
    {
        //READ ALL DATA//
        [HttpGet]
        [Route("api/province")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all provinces
                var province = db.PROVINCEs.Select(x => new { x.PROVINCEID, x.PROVINCENAME }).ToList();

                if (province == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(province);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/province")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified province
                var province = db.PROVINCEs.Where(z => z.PROVINCEID == id).Select(x => new { x.PROVINCEID, x.PROVINCENAME }).FirstOrDefault();

                if (province == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(province);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //ADD//
        [HttpPost]
        [Route("api/province")]
        public IHttpActionResult Post([FromUri] string provincename)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Null checks
                if (string.IsNullOrEmpty(provincename))
                    return BadRequest();

                //Add a province
                db.PROVINCEs.Add(new PROVINCE
                {
                    PROVINCENAME = provincename
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
        [Route("api/province")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string provincename)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var provinces = db.PROVINCEs.FirstOrDefault(x => x.PROVINCEID == id);

                //Null checks
                if (string.IsNullOrEmpty(provincename))
                    return BadRequest();

                //Update specified province
                provinces.PROVINCENAME = provincename;

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
        [Route("api/province")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;

                //Find province
                var province = db.PROVINCEs.FirstOrDefault(x => x.PROVINCEID == id);
                if (province == null)
                    return NotFound();

                //Delete specified province
                db.PROVINCEs.Remove(province);

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
