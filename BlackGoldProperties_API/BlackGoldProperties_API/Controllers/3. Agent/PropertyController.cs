using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._3._Agent
{
    public class PropertyController : ApiController
    {
        //READ ALL DATA//
        [HttpGet]
        [Route("api/property")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all properties 
                var property = db.PROPERTies.Select(x => new {
                    x.PROPERTYID,
                    x.PROPERTYADDRESS,
                    Price = x.PRICEs.Select(y => new { y.PRICEID, y.PRICEAMOUNT, y.PRICEDATE}).ToList(),  //--Fix this to read the last price in the DB for that property 
                    x.PROPERTYOWNER.PROPERTYOWNERID,
                    x.PROPERTYOWNER.PROPERTYOWNERNAME,
                    x.PROPERTYOWNER.PROPERTYOWNERSURNAME,
                    x.PROPERTYOWNER.PROPERTYOWNEREMAIL,
                    x.PROPERTYOWNER.PROPERTYOWNERADDRESS,
                    x.PROPERTYOWNER.PROPERTYOWNERIDNUMBER,
                    x.PROPERTYOWNER.PROPERTYOWNERCONTACTNUMBER,
                    x.PROPERTYOWNER.PROPERTYOWNERALTCONTACTNUMBER,
                    PropertyFeatures = x.PROPERTYFEATUREs.Select(y => new { y.FEATURE.FEATUREID, y.FEATURE.FEATUREDESCRIPTION, y.PROPERTYFEATUREQUANTITY }).ToList(),
                    Pointsofinterest = x.SUBURB.SUBURBPOINTOFINTERESTs.Select(y => new { y.SUBURB.SUBURBID, y.SUBURB.SUBURBNAME, y.POINTOFINTEREST.POINTOFINTERESTID, y.POINTOFINTEREST.POINTOFINTERESTNAME, y.POINTOFINTEREST.POINTOFINTERESTTYPE.POINTOFINTERESTTYPEID, y.POINTOFINTEREST.POINTOFINTERESTTYPE.POINTOFINTERESTTYPEDESCRIPTION }).ToList(),
                    Mandates = x.PROPERTYMANDATEs.Select(y => new { y.MANDATE.MANDATEID, y.MANDATE.MANDATEDATE, y.MANDATE.MANDATEDOCUMENT, y.MANDATE.MANDATETYPE.MANDATETYPEID, y.MANDATE.MANDATETYPE.MANDATETYPEDESCRIPTION }).ToList(),
                    x.MARKETTYPE.MARKETTYPEID,
                    x.MARKETTYPE.MARKETTYPEDESCRIPTION,
                    x.PROPERTYTYPE.PROPERTYTYPEID,
                    x.PROPERTYTYPE.PROPERTYTYPEDESCRIPTION,
                    x.SUBURB.SUBURBID,
                    x.SUBURB.SUBURBNAME,
                    x.SUBURB.CITY.CITYID,
                    x.SUBURB.CITY.CITYNAME,
                    x.SUBURB.CITY.PROVINCE.PROVINCEID,
                    x.SUBURB.CITY.PROVINCE.PROVINCENAME,
                    Spaces = x.PROPERTYSPACEs.Select(y => new { y.SPACE.SPACEID, y.SPACE.SPACEDESCRIPTION, y.PROPERTYSPACEQUANTITY }).ToList(),
                    Otherbuildingdetails = x.PROPERTYOTHERBUILDINGDETAILs.Select(y => new {y.OTHERBUILDINGDETAIL.OTHERBUILDINGDETAILID, y.OTHERBUILDINGDETAIL.OTHERBUILDINGDETAILDESCRIPTION}).ToList(),
                }).ToList();

                if (property == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(property);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/property")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified property
                var property = db.PROPERTies.Where(z => z.PROPERTYID == id).Select(x => new {
                    x.PROPERTYID,
                    x.PROPERTYADDRESS,
                    Price = x.PRICEs.Select(y => new { y.PRICEID, y.PRICEAMOUNT, y.PRICEDATE }).ToList(), //--Fix this to read the last price in the DB for that property 
                    x.PROPERTYOWNER.PROPERTYOWNERID,
                    x.PROPERTYOWNER.PROPERTYOWNERNAME,
                    x.PROPERTYOWNER.PROPERTYOWNERSURNAME,
                    x.PROPERTYOWNER.PROPERTYOWNEREMAIL,
                    x.PROPERTYOWNER.PROPERTYOWNERADDRESS,
                    x.PROPERTYOWNER.PROPERTYOWNERIDNUMBER,
                    x.PROPERTYOWNER.PROPERTYOWNERCONTACTNUMBER,
                    x.PROPERTYOWNER.PROPERTYOWNERALTCONTACTNUMBER,
                    PropertyFeatures = x.PROPERTYFEATUREs.Select(y => new { y.FEATURE.FEATUREID, y.FEATURE.FEATUREDESCRIPTION, y.PROPERTYFEATUREQUANTITY }).ToList(),
                    Pointsofinterest = x.SUBURB.SUBURBPOINTOFINTERESTs.Select(y => new { y.SUBURB.SUBURBID, y.SUBURB.SUBURBNAME, y.POINTOFINTEREST.POINTOFINTERESTID, y.POINTOFINTEREST.POINTOFINTERESTNAME, y.POINTOFINTEREST.POINTOFINTERESTTYPE.POINTOFINTERESTTYPEID, y.POINTOFINTEREST.POINTOFINTERESTTYPE.POINTOFINTERESTTYPEDESCRIPTION }).ToList(),
                    Mandates = x.PROPERTYMANDATEs.Select(y => new { y.MANDATE.MANDATEID, y.MANDATE.MANDATEDATE, y.MANDATE.MANDATEDOCUMENT, y.MANDATE.MANDATETYPE.MANDATETYPEID, y.MANDATE.MANDATETYPE.MANDATETYPEDESCRIPTION }).ToList(),
                    x.MARKETTYPE.MARKETTYPEID,
                    x.MARKETTYPE.MARKETTYPEDESCRIPTION,
                    x.PROPERTYTYPE.PROPERTYTYPEID,
                    x.PROPERTYTYPE.PROPERTYTYPEDESCRIPTION,
                    x.SUBURB.SUBURBID,
                    x.SUBURB.SUBURBNAME,
                    x.SUBURB.CITY.CITYID,
                    x.SUBURB.CITY.CITYNAME,
                    x.SUBURB.CITY.PROVINCE.PROVINCEID,
                    x.SUBURB.CITY.PROVINCE.PROVINCENAME,
                    Spaces = x.PROPERTYSPACEs.Select(y => new { y.SPACE.SPACEID, y.SPACE.SPACEDESCRIPTION, y.PROPERTYSPACEQUANTITY }).ToList(),
                    Otherbuildingdetails = x.PROPERTYOTHERBUILDINGDETAILs.Select(y => new { y.OTHERBUILDINGDETAIL.OTHERBUILDINGDETAILID, y.OTHERBUILDINGDETAIL.OTHERBUILDINGDETAILDESCRIPTION }).ToList(),
                }).FirstOrDefault();

                if (property == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(property);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        //ADD//   --- MANDATE DOC to be uploaded + listingimage  --- Havent done mandate in here at all yet.. find out why userid is in mandate(STEPS: add mandate first to mandate entity, then add to associative entity)   -- take note that points of interest are auto added when the suburb is chosen
        [HttpPost]
        [Route("api/property")]
        public IHttpActionResult Post([FromUri] string address, [FromUri] decimal price, [FromUri] string ownername, [FromUri] string ownersurname, [FromUri] string owneremail, [FromUri] string owneraddress, [FromUri] string owneridnumber, [FromUri] string ownerpassport, [FromUri] string ownercontactnumber, [FromUri] string owneraltcontactnumber, [FromUri] int markettypeid, [FromUri] int propertytypeid, [FromUri] int suburbid, /*[FromUri] int mandatetypeid, [FromUri] DateTime mandatedate,*/ [FromUri] int agentid, [FromBody] dynamic propertydetails)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Null checks   --- FINISH THIS
                if (string.IsNullOrEmpty(address))
                    return BadRequest();

                //Add the property owner
                db.PROPERTYOWNERs.Add(new PROPERTYOWNER
                {
                    PROPERTYOWNERNAME = ownername,
                    PROPERTYOWNERSURNAME = ownersurname,
                    PROPERTYOWNEREMAIL = owneremail,
                    PROPERTYOWNERIDNUMBER = owneridnumber, // --this should allow for null in the DB
                    PROPERTYOWNERPASSPORTNUMBER = ownerpassport,
                    PROPERTYOWNERADDRESS = owneraddress,
                    PROPERTYOWNERCONTACTNUMBER = ownercontactnumber,
                    PROPERTYOWNERALTCONTACTNUMBER = owneraltcontactnumber
                });

                //Save DB changes
                db.SaveChanges();

                //Get newly registered property owner
                int lastownerid = db.PROPERTYOWNERs.Max(item => item.PROPERTYOWNERID);

                //Add a property
                db.PROPERTies.Add(new PROPERTY
                {
                    SUBURBID = suburbid,
                    MARKETTYPEID = markettypeid,
                    PROPERTYSTATUSID = 1, //property is flagged as 'Available'
                    PROPERTYOWNERID = lastownerid, //assign newly registered property owner to the property
                    USERID = agentid,
                    PROPERTYTYPEID = propertytypeid,
                    PROPERTYADDRESS = address
                });

                //Save DB changes
                db.SaveChanges();

                //Get newly added property
                int lastpropertyid = db.PROPERTies.Max(item => item.PROPERTYID);

                //Add the price to the property
                db.PRICEs.Add(new PRICE
                {
                    PROPERTYID = lastpropertyid,
                    PRICEAMOUNT = price,
                    PRICEDATE = DateTime.Now //Sets the price datetime to the current date and time
                });

                //Add other building details to the property 
                foreach (dynamic item in propertydetails.otherbuildingdetails)
                {
                    db.PROPERTYOTHERBUILDINGDETAILs.Add(new PROPERTYOTHERBUILDINGDETAIL
                    {
                        PROPERTYID = lastpropertyid,
                        OTHERBUILDINGDETAILID = item.otherbuildingdetailid
                    });
                }

                //Add features to the property
                foreach (dynamic item in propertydetails.propertyfeatures)
                {
                    db.PROPERTYFEATUREs.Add(new PROPERTYFEATURE
                    {
                        PROPERTYID = lastpropertyid,
                        FEATUREID = item.featureid,
                        PROPERTYFEATUREQUANTITY = item.propertyfeaturequanity
                    });
                }

                //Add spaces to the property
                foreach (dynamic item in propertydetails.propertyspaces)
                {
                    db.PROPERTYSPACEs.Add(new PROPERTYSPACE
                    {
                        PROPERTYID = lastpropertyid,
                        SPACEID = item.spaceid,
                        PROPERTYSPACEQUANTITY = item.propertyspacequanity
                    });
                }

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
        [Route("api/property")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string address, [FromUri] decimal price, [FromUri] string ownername, [FromUri] string ownersurname, [FromUri] string owneremail, [FromUri] string owneraddress, [FromUri] string owneridnumber, [FromUri] string ownerpassport, [FromUri] string ownercontactnumber, [FromUri] string owneraltcontactnumber, [FromUri] int markettypeid, [FromUri] int propertytypeid, [FromUri] int suburbid, /*[FromUri] int mandatetypeid, [FromUri] DateTime mandatedate,*/ [FromUri] int agentid, [FromBody] dynamic propertydetails)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var properties = db.PROPERTies.FirstOrDefault(x => x.PROPERTYID == id);

                //Null checks   --Finish this
                if (string.IsNullOrEmpty(address))
                    return BadRequest();

                //Update specified property
                properties.PROPERTYOWNER.PROPERTYOWNERNAME = ownername;
                properties.PROPERTYOWNER.PROPERTYOWNERSURNAME = ownersurname;
                properties.PROPERTYOWNER.PROPERTYOWNEREMAIL = owneremail;
                properties.PROPERTYOWNER.PROPERTYOWNERADDRESS = owneraddress;
                properties.PROPERTYOWNER.PROPERTYOWNERIDNUMBER = owneridnumber;
                properties.PROPERTYOWNER.PROPERTYOWNERPASSPORTNUMBER = ownerpassport;
                properties.PROPERTYOWNER.PROPERTYOWNERCONTACTNUMBER = ownercontactnumber;
                properties.PROPERTYOWNER.PROPERTYOWNERALTCONTACTNUMBER = owneraltcontactnumber;
                properties.SUBURBID = suburbid;
                properties.MARKETTYPEID = markettypeid;
                properties.PROPERTYTYPEID = propertytypeid;
                properties.USERID = agentid;
                properties.PROPERTYADDRESS = address;

                //Add the updated price to the property without deleting the old price
                db.PRICEs.Add(new PRICE
                {
                    PROPERTYID = id,
                    PRICEAMOUNT = price,
                    PRICEDATE = DateTime.Now //Set the price datetime to the current date and time
                });


                //Find all associative records for other building details
                var otherbuildingdetails = db.PROPERTYOTHERBUILDINGDETAILs.Where(x => x.PROPERTYID == id);

                //Delete other building details records
                foreach (var item in otherbuildingdetails)
                {
                    db.PROPERTYOTHERBUILDINGDETAILs.Remove(item);
                }

                //Add updated other building details to the property 
                foreach (dynamic item in propertydetails.otherbuildingdetails)
                {
                    db.PROPERTYOTHERBUILDINGDETAILs.Add(new PROPERTYOTHERBUILDINGDETAIL
                    {
                        PROPERTYID = id,
                        OTHERBUILDINGDETAILID = item.otherbuildingdetailid
                    });
                }


                //Find all associative records for features
                var features = db.PROPERTYFEATUREs.Where(x => x.PROPERTYID == id);

                //Delete features records
                foreach (var item in features)
                {
                    db.PROPERTYFEATUREs.Remove(item);
                }

                //Add updated features to the property 
                foreach (dynamic item in propertydetails.propertyfeatures)
                {
                    db.PROPERTYFEATUREs.Add(new PROPERTYFEATURE
                    {
                        PROPERTYID = id,
                        FEATUREID = item.featureid,
                        PROPERTYFEATUREQUANTITY = item.propertyfeaturequanity
                    });
                }


                //Find all associative records for spaces
                var spaces = db.PROPERTYSPACEs.Where(x => x.PROPERTYID == id);

                //Delete spaces records
                foreach (var item in spaces)
                {
                    db.PROPERTYSPACEs.Remove(item);
                }

                //Add updated spaces to the property 
                foreach (dynamic item in propertydetails.propertyspaces)
                {
                    db.PROPERTYSPACEs.Add(new PROPERTYSPACE
                    {
                        PROPERTYID = id,
                        SPACEID = item.spaceid,
                        PROPERTYSPACEQUANTITY = item.propertyspacequanity
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


        //DELETE//
        [HttpDelete]
        [Route("api/property")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;

                //Find property
                var property = db.PROPERTies.FirstOrDefault(x => x.PROPERTYID == id);
                if (property == null)
                    return NotFound();


                //Find all associative records for other building details
                var otherbuildingdetails = db.PROPERTYOTHERBUILDINGDETAILs.Where(x => x.PROPERTYID == id);

                //Delete other building details records
                foreach (var item in otherbuildingdetails)
                {
                    db.PROPERTYOTHERBUILDINGDETAILs.Remove(item);
                }


                //Find all associative records for features
                var features = db.PROPERTYFEATUREs.Where(x => x.PROPERTYID == id);

                //Delete features records
                foreach (var item in features)
                {
                    db.PROPERTYFEATUREs.Remove(item);
                }


                //Find all associative records for spaces
                var spaces = db.PROPERTYSPACEs.Where(x => x.PROPERTYID == id);

                //Delete spaces records
                foreach (var item in spaces)
                {
                    db.PROPERTYSPACEs.Remove(item);
                }


                //Find all associative records for spaces
                var prices = db.PRICEs.Where(x => x.PROPERTYID == id);

                //Delete spaces records
                foreach (var item in prices)
                {
                    db.PRICEs.Remove(item);
                }

                //Save DB Changes
                db.SaveChanges();

                //Delete specified property
                db.PROPERTies.Remove(property);

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
