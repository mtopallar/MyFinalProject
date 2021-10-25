using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; } //Appsettings.json daki token optionsları okumak için.
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //oku ve TokenOptions a map le

        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer, //token payload'undaki registered (reserved) clam: iss
                audience: tokenOptions.Audience, // token payload'undaki registered (reserved) clam: aud
                expires: _accessTokenExpiration, // token payload'undaki registered (reserved) clam: exp
                notBefore: DateTime.Now, // token payload'undaki registered (reserved) clam: nbf
                claims: SetClaims(user, operationClaims), // token payload'ındaki bizim verdiğimiz bilgiler.
                signingCredentials: signingCredentials // token ın son kısmında (signature) kullanılacak algoritma ve secret key bilgileri
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }

        /* SetClaims metodundaki bilgiler de token ın payload kısmındaki claimler içine ekleniyor. CreateJwtSecurityToken metodundaki claim alanlarında sadece reserved claimler in değerleri bir set edilirken bu metodda da reserved olmayan, kullanıcıya ve rollerine ait bilgiler örneğin mail, user id, kullanıcının rolleri gibi veriler set diliyor. */
    }
}
