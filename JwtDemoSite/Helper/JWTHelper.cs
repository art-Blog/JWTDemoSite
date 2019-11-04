using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace JwtDemoSite.Helper
{
    public class JWTHelper
    {
        private const string Secret =
            "SjuXhiUgltqTPgJSBLn6Il2IsNN0WUydRVpjb8BkwoHcrEjQveFSNdBVW1NovijJXi1yFIlZxDbrm2ROMC8Fjw==";

        /// <summary>
        /// 產生Token
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="expireMinutes">有效時間</param>
        /// <returns></returns>
        public static string GenerateToken(ClaimsIdentity identity, int expireMinutes = 20)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = now.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            return token;
        }

        /// <summary>
        /// 解析Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken == null) return null;

                var symmetricKey = Convert.FromBase64String(Secret);
                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}