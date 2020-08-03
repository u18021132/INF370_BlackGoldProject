using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Controllers._8._Employee_Administration
{
    public class EmployeeController : ApiController
    {
        //READ ALL DATA//     -- Fix DELETE
        [HttpGet]
        [Route("api/employee")]
        public IHttpActionResult Get()
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get all employees
                var employee = db.EMPLOYEEs.Select(x => new { 
                    x.USER.USERID, 
                    x.USER.USERNAME,
                    x.USER.USERSURNAME,
                    x.USER.USERCONTACTNUMBER,
                    x.USER.USERALTCONTACTNUMBER,
                    x.USER.USEREMAIL,
                    x.USER.USERIDNUMBER,
                    x.USER.USERADDRESS,
                    x.EMPLOYEEBANKINGDETAILS,
                    EmployeeType = x.EMPLOYEEROLEs.Select(y => new {y.EMPLOYEETYPE.EMPLOYEETYPEID, y.EMPLOYEETYPE.EMPLOYEETYPEDESCRIPTION }).ToList(),
                    x.EMPLOYEEDATEEMPLOYED,
                    x.EMPLOYEERENUMERATON
                }).ToList();

                if (employee == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(employee);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //READ DATA OF SPECIFIC ID//
        [HttpGet]
        [Route("api/employee")]
        public IHttpActionResult Get([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                db.Configuration.ProxyCreationEnabled = false;

                //Get specified employee
                var employee = db.EMPLOYEEs.Where(z => z.USERID == id).Select(x => new {
                    x.USER.USERID,
                    x.USER.USERNAME,
                    x.USER.USERSURNAME,
                    x.USER.USERCONTACTNUMBER,
                    x.USER.USERALTCONTACTNUMBER,
                    x.USER.USEREMAIL,
                    x.USER.USERIDNUMBER,
                    x.USER.USERADDRESS,
                    x.EMPLOYEEBANKINGDETAILS,
                    EmployeeType = x.EMPLOYEEROLEs.Select(y => new {y.EMPLOYEETYPE.EMPLOYEETYPEID, y.EMPLOYEETYPE.EMPLOYEETYPEDESCRIPTION }).ToList(),
                    x.EMPLOYEEDATEEMPLOYED,
                    x.EMPLOYEERENUMERATON
                }).FirstOrDefault();

                if (employee == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(employee);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        //ADD//    --Maybe make employeeroles dynamic so that no classes need to be declared in angular
        [HttpPost]
        [Route("api/employee")]
        public IHttpActionResult Post([FromUri] string name, [FromUri] string surname, [FromUri] string contactnumber, [FromUri] string altcontactnumber, [FromUri] string email,[FromUri] string idnumber, [FromUri] string passportnumber, [FromUri] string address, [FromUri] string password, [FromUri] string banking, [FromBody] EMPLOYEEROLE[] employeeroles, [FromUri] DateTime dateemployed, [FromUri] decimal remuneration)
        {

            //DB context
            var db = LinkToDBController.db;
            db.Configuration.ProxyCreationEnabled = false;

            //Null checks
            if (string.IsNullOrEmpty(email))
                return BadRequest();
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            if (string.IsNullOrEmpty(surname))
                return BadRequest();
            if (string.IsNullOrEmpty(address))
                return BadRequest();
            if (string.IsNullOrEmpty(password))
                return BadRequest();


            //Add an employee 
            db.USERs.Add(new USER
            {
                USEREMAIL = email,
                USERPASSWORD = password, //--change this to be hashed
                USERNAME = name,
                USERSURNAME = surname,
                USERCONTACTNUMBER = contactnumber,
                USERALTCONTACTNUMBER = altcontactnumber,
                USERIDNUMBER = idnumber,
                USERPASSPORTNUMBER = passportnumber,
                USERADDRESS = address
            });

            //Save DB changes
            db.SaveChanges();

            //Find the user id that was just registered
            int lastuserid = db.USERs.Max(item => item.USERID);

            //Link the user profile to employee
            db.EMPLOYEEs.Add(new EMPLOYEE
            {
                USERID = lastuserid,
                EMPLOYEEBANKINGDETAILS = banking,
                EMPLOYEEDATEEMPLOYED = dateemployed,
                EMPLOYEERENUMERATON = remuneration
            });

            //Save DB changes
            db.SaveChanges();

            //Assign the user roles to the employee
            foreach (EMPLOYEEROLE item in employeeroles)
            {
                db.EMPLOYEEROLEs.Add(new EMPLOYEEROLE
                {
                    USERID = lastuserid,
                    EMPLOYEETYPEID = item.EMPLOYEETYPEID   
                });
            }          

            //Save DB changes
            db.SaveChanges();

            //Return ok
            return Ok();
        }


        //UPDATE//
        [HttpPatch]
        [Route("api/employee")]
        public IHttpActionResult Patch([FromUri] int id, [FromUri] string name, [FromUri] string surname, [FromUri] string contactnumber, [FromUri] string altcontactnumber, [FromUri] string email, [FromUri] string idnumber, [FromUri] string passportnumber, [FromUri] string address, [FromUri] string password, [FromUri] string banking, [FromBody] EMPLOYEEROLE[] employeeroles, [FromUri] DateTime dateemployed, [FromUri] decimal remuneration)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;
                var employees = db.EMPLOYEEs.FirstOrDefault(x => x.USERID == id);

                //Null checks   --Finish this
                if (string.IsNullOrEmpty(name))
                    return BadRequest();

                //Update specified employee
                employees.USER.USEREMAIL = email;
                employees.USER.USERPASSWORD = password; //--change this to be hashed
                employees.USER.USERNAME = name;
                employees.USER.USERSURNAME = surname;
                employees.USER.USERCONTACTNUMBER = contactnumber;
                employees.USER.USERALTCONTACTNUMBER = altcontactnumber;
                employees.USER.USERIDNUMBER = idnumber;
                employees.USER.USERPASSPORTNUMBER = passportnumber;
                employees.USER.USERADDRESS = address;
                employees.EMPLOYEEBANKINGDETAILS = banking;
                employees.EMPLOYEEDATEEMPLOYED = dateemployed;
                employees.EMPLOYEERENUMERATON = remuneration;

                //Find all associative records for employee roles
                var roles = db.EMPLOYEEROLEs.Where(x => x.USERID == id);

                //Delete employee roles records
                foreach (var item in roles)
                {
                    db.EMPLOYEEROLEs.Remove(item);
                }

                //Add updated employee roles to the employee 
                foreach (EMPLOYEEROLE item in employeeroles)
                {
                    db.EMPLOYEEROLEs.Add(new EMPLOYEEROLE
                    {
                        USERID = id,
                        EMPLOYEETYPEID = item.EMPLOYEETYPEID
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

        //DELETE//    -DELETE ISNT WORKING 
        [HttpDelete]
        [Route("api/employee")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                //DB context
                var db = LinkToDBController.db;

                //Find all associative records for employee roles
                var roles = db.EMPLOYEEROLEs.Where(x => x.USERID == id);

                //Delete employee roles records
                foreach (var item in roles)
                {
                    db.EMPLOYEEROLEs.Remove(item);
                }

                //Save DB Changes
                db.SaveChanges();

                //Find employee
                var employee = db.EMPLOYEEs.FirstOrDefault(x => x.USERID == id);
                if (employee == null)
                    return NotFound();

                //Delete specified employee
                db.EMPLOYEEs.Remove(employee);

                //Save DB Changes
                db.SaveChanges();

                //Find user
                var user = db.USERs.FirstOrDefault(x => x.USERID == id);
                if (user == null)
                    return NotFound();

                //Delete specified employee
                db.USERs.Remove(user);

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
