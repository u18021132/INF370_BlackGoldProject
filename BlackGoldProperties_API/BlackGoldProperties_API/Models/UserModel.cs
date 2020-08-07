using System;
using System.Linq;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Models
{
    public class UserModel
    {
        public USER user;

        public void RefreshGUID(BlackGoldDBEntities10 db)
        {
            db.Configuration.ProxyCreationEnabled = false;
            user.USERGUID = Guid.NewGuid().ToString();
            user.USERGUIDEXPIRY = DateTime.Now.AddMinutes(30);
            var guids = db.USERs.Where(x => x.USERGUID == user.USERGUID).Count();
            if (guids > 0)
                RefreshGUID(db);
            else
            {
                var usr = db.USERs.Where(x => x.USERID == user.USERID).FirstOrDefault();
                db.Entry(usr).CurrentValues.SetValues(user);
                try
                {
                    db.SaveChanges();
                }
                catch
                {

                }
            }
        }
    }
}