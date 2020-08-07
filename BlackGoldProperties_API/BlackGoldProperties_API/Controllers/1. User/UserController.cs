using System.Collections.Generic;
using System.Web.Http;
using BlackGoldProperties_API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace BlackGoldProperties_API.Controllers._1._User
{
    public class UserController : ApiController
    {
        //[HttpPost]
        //public USER GetUser(dynamic token, int id)
        //{
        //    var db = LinkToDBController.db;
        //    if (TokenManager.Validate(token) != true)
        //        return null;
        //    db.Configuration.ProxyCreationEnabled = false;
        //    USER user = db.USERs.Find(id);
        //    if (user == null)
        //        return null;
        //    return user;
        //}

        //[HttpPatch]
        //public dynamic AddUser(USER user)
        //{
        //    var db = LinkToDBController.db;
        //    db.Configuration.ProxyCreationEnabled = false;
        //    if (!ModelState.IsValid)
        //        return null;
        //    user = GUIDActions(user);
        //    string originalPassword = user.USERPASSWORD;
        //    user.USERPASSWORD = HomeController.HashPassword(user.USERPASSWORD);
        //    db.USERs.Add(user);
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        return e;
        //    }
        //    user.USERPASSWORD = originalPassword;
        //    dynamic userToken = Login(user);
        //    return userToken;
        //}

        //LOGIN//
        [HttpPost]
        [Route("api/user")]
        public dynamic Login([FromUri] string email, [FromUri] string password)
        {
            var db = LinkToDBController.db;
            db.Configuration.ProxyCreationEnabled = false;
            if (email == null || password == null)
                return null;
            if (!ModelState.IsValid)
                return null;
            dynamic userToken = HomeController.Login(email, password);
            if (userToken == null)
                return null;
            return userToken;
        }

        //[HttpPut]
        //public USER UpdateUser(int id, List<JObject> updateList)
        //{
        //    var db = LinkToDBController.db;
        //    db.Configuration.ProxyCreationEnabled = false;
        //    USER user = JsonConvert.DeserializeObject<USER>(updateList[0].ToString());
        //    string token = HomeController.GetToken(updateList);
        //    if (TokenManager.Validate(token) != true)
        //        return null;
        //    if (!ModelState.IsValid)
        //        return null;
        //    if (id != user.USERID)
        //        return null;
        //    user = GUIDActions(user);
        //    user.USERPASSWORD = HomeController.HashPassword(user.USERPASSWORD);
        //    var e = db.USERs.Find(user.USERID);
        //    try
        //    {
        //        db.Entry(e).CurrentValues.SetValues(user);
        //        db.SaveChanges();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    return user;
        //}

        //[HttpDelete]
        //public string DeleteUser(int id, dynamic token)
        //{
        //    var db = LinkToDBController.db;
        //    if (TokenManager.Validate(token) != true)
        //        return null;
        //    db.Configuration.ProxyCreationEnabled = false;
        //    if (!ModelState.IsValid)
        //        return null;
        //    var user = db.USERs.Where(e => e.USERID == id).FirstOrDefault();
        //    try
        //    {

        //        db.USERs.Remove(user);
        //        db.SaveChanges();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    return "Deleted";

        //}

    }
}
