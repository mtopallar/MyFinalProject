using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        //web apinin kullanacağı jwt lerin oluştutulması için elimizde olanlar credential dır (kullanıcı adı ve şifre gibi). asp.net in token doğrulaması için yazılan bir class bu. JWT için güveblik anahtarın ve algoritman budur.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
