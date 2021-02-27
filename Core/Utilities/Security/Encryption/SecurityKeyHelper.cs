using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
   public class SecurityKeyHelper
    {
        //işin içinde şifreleme varsa her şeyi byte[] formatında vermelisin. JWT servisleri başka türlü anlamayacaktır. (Appsettingsjson daki key)

        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
