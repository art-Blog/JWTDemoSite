using System;
using System.Configuration;
using System.Security.Claims;
using System.Text;
using JwtDemoSite.Helper;
using JwtDemoSite.Models;
using Newtonsoft.Json;

namespace JwtDemoSite.Modules.Token.Implement
{
    internal class ArtTokenModule : ITokenModule
    {
        // REF:https://ithelp.ithome.com.tw/articles/10199102

        // token結構改為 iv.payload.signature
        protected string Key = ConfigurationManager.AppSettings["JWTKey"];

        public string GenerateToken(object identity, int expireMinutes = 20)
        {
            var exp = expireMinutes * 60;   //過期時間(秒)

            //稍微修改 Payload 將使用者資訊和過期時間分開
            var payload = new Payload
            {
                info = (User)identity,
                //Unix 時間戳
                exp = Convert.ToInt32((DateTime.Now.AddSeconds(exp) - new DateTime(1970, 1, 1)).TotalSeconds)
            };

            var json = JsonConvert.SerializeObject(payload);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            var iv = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);

            //使用 AES 加密 Payload
            var encrypt = TokenCrypto.AESEncrypt(base64, Key.Substring(0, 16), iv);

            //取得簽章
            var signature = TokenCrypto.ComputeHMACSHA256($"{iv}.{encrypt}", Key.Substring(0, 64));

            return $"{iv}.{encrypt}.{signature}";
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            var split = token.Split('.');
            var iv = split[0];
            var encrypt = split[1];
            var signature = split[2];

            //檢查簽章是否正確
            if (signature != TokenCrypto.ComputeHMACSHA256($"{iv}.{encrypt}", Key.Substring(0, 64)))
            {
                return null;
            }

            //使用 AES 解密 Payload
            var base64 = TokenCrypto.AESDecrypt(encrypt, Key.Substring(0, 16), iv);
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
            var payload = JsonConvert.DeserializeObject<Payload>(json);

            //檢查是否過期
            if (payload.exp < Convert.ToInt32(
                (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds))
            {
                return null;
            }

            var claims = new ClaimsIdentity(
                  new[]
                  {
                    new Claim(ClaimTypes.NameIdentifier, payload.info.EmployeeNo.ToString()),
                    new Claim(ClaimTypes.Name, payload.info.UserName),
                    new Claim("Account", payload.info.Account),
                    new Claim(ClaimTypes.Email, payload.info.EmailAccount)
                  });

            var result = new ClaimsPrincipal(claims);
            return result;
        }
    }
}