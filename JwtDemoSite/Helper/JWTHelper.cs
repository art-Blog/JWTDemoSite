using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace JwtDemoSite.Helper
{
    public class JWTHelper
    {
        private static readonly string Key = ConfigurationManager.AppSettings["JWTKey"];

        /// <summary>
        /// 產生Token
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="expireMinutes">有效時間</param>
        /// <returns></returns>
        public static string GenerateToken(ClaimsIdentity identity, int expireMinutes = 20)
        {
            var symmetricKey = Convert.FromBase64String(Key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            // 如果發生例外，會因為歐盟的GDPR法規關係，微軟預設是會將可能包含個資的例外擋掉
            // 解決方式是設定允許顯示PII
            //IdentityModelEventSource.ShowPII = true;
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

                var symmetricKey = Convert.FromBase64String(Key);
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