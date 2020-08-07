using System;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlackGoldProperties_API.Controllers;

namespace BlackGoldProperties_API.Models
{
    public class TokenManager
    {
        dynamic db = new LinkToDBController();
        private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";

        public class Token
        {
            public string token { get; set; }
        }
        public static string GenerateToken(string username)
        {
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Name, username)}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public static bool Validate(string token)
        {
            var db = LinkToDBController.db;
            db.Configuration.ProxyCreationEnabled = false;
            string tokenUseremail = ValidateToken(token);
            if (db.USERs.Where(x => x.USEREMAIL == tokenUseremail).FirstOrDefault() == null)
                return false;
            return true;
        }

        public static string ValidateToken(string token)
        {
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
            string useremail = usernameClaim.Value;
            return useremail;
        }

        public static dynamic GetRoles(string token)
        {
            var db = LinkToDBController.db;
            db.Configuration.ProxyCreationEnabled = false;
            var useremail = ValidateToken(token);
            USER user = db.USERs.Where(x => x.USEREMAIL == useremail).FirstOrDefault();
            dynamic roleList = user.EMPLOYEE.EMPLOYEEROLEs;
            foreach (EMPLOYEEROLE role in roleList)
            {
                roleList.Add(role.EMPLOYEETYPE.EMPLOYEETYPEDESCRIPTION);
            }
            return roleList;
        }

        public static bool IsLoggedIn(string token)
        {
            var db = LinkToDBController.db;
            db.Configuration.ProxyCreationEnabled = false;
            var useremail = ValidateToken(token);
            var guids = db.USERs.Where(x => x.USERGUID == useremail && x.USERGUIDEXPIRY > DateTime.Now).Count();
            if (guids > 0)
                return true;
            return false;
        }
    }
}