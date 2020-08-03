using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace BlackGoldProperties_API.Controllers._4._Inspection_Administration
{
    public class InspectionController : ApiController
    {
        //READ ALL DATA// 
        [HttpGet]
        [Route("api/inspection")]   
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all inspections
                var inspections = db.INSPECTIONs.Select(x => new { 
                    x.INSPECTIONID, 
                    x.INSPECTIONDATE, 
                    x.INSPECTIONDOCUMENT, 
                    x.INSPECTIONCOMMENT, 
                    x.INSPECTIONTYPE.INSPECTIONTYPEID, 
                    x.INSPECTIONTYPE.INSPECTIONTYPEDESCRIPTION, 
                    x.PROPERTY.PROPERTYID, 
                    x.PROPERTY.PROPERTYADDRESS, 
                    x.EMPLOYEE.USER.USERID, 
                    x.EMPLOYEE.USER.USERNAME, 
                    x.EMPLOYEE.USER.USERSURNAME, 
                    x.IVSTATU.IVSTATUSID, 
                    x.IVSTATU.IVSTATUSDESCRIPTION, 
                    PropertyDefects = x.PROPERTYDEFECTs.Select(y => new { y.DEFECT.DEFECTDESCRIPTION, y.PROPERTYDEFECTQUANTITY }).ToList() 
                }).ToList();

                if (inspections == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(inspections);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/inspection")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified inspection
                var inspection = db.INSPECTIONs.Where(z => z.INSPECTIONID == id).Select(x => new { 
                    x.INSPECTIONID, 
                    x.INSPECTIONDATE, 
                    x.INSPECTIONDOCUMENT, 
                    x.INSPECTIONCOMMENT, 
                    x.INSPECTIONTYPE.INSPECTIONTYPEID, 
                    x.INSPECTIONTYPE.INSPECTIONTYPEDESCRIPTION, 
                    x.PROPERTY.PROPERTYID, 
                    x.PROPERTY.PROPERTYADDRESS, 
                    x.EMPLOYEE.USER.USERID, 
                    x.EMPLOYEE.USER.USERNAME,  
                    x.EMPLOYEE.USER.USERSURNAME, 
                    x.IVSTATU.IVSTATUSID, 
                    x.IVSTATU.IVSTATUSDESCRIPTION, 
                    PropertyDefects = x.PROPERTYDEFECTs.Select(y => new { y.DEFECT.DEFECTDESCRIPTION, y.PROPERTYDEFECTQUANTITY }).ToList() 
                }).FirstOrDefault();

                if (inspection == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(inspection);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //CAPTURE INSPECTION//
        [HttpPatch]
        [Route("api/inspection")]   //---- Find a way to handle the pdf request?? +  fix USERID  not being able to update
        public IHttpActionResult Patch([FromUri] int id, [FromUri] DateTime date, [FromUri] string comment, [FromUri] int typeid, [FromUri] int userid, [FromUri] int IVid,[FromBody] PROPERTYDEFECT[] propertydefect)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var inspection = db.INSPECTIONs.FirstOrDefault(x => x.INSPECTIONID == id);

                //Null checks
                //if (string.IsNullOrEmpty(description))
                // return BadRequest();

                //Capture inspection    
                inspection.INSPECTIONDATE = date;
                inspection.INSPECTIONCOMMENT = comment;
                inspection.INSPECTIONTYPEID = typeid;
                inspection.USERID = userid;
                inspection.IVSTATUSID = IVid;
                foreach (PROPERTYDEFECT item in propertydefect)
                {
                    db.PROPERTYDEFECTs.Add(new PROPERTYDEFECT
                    {
                        DEFECTID = item.DEFECTID,
                        PROPERTYDEFECTQUANTITY = item.PROPERTYDEFECTQUANTITY,
                        INSPECTIONID = id
                    });
                }


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
    }
}
