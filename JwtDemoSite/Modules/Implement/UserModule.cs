using System.Security.Claims;
using JwtDemoSite.Models;

namespace JwtDemoSite.Modules.Implement
{
    public class UserModule : IUserModule
    {
        public User VerifyUser(string account, string password)
        {
            if (account.ToLower() == "art" && password == "1234")
            {
                // 此處應該要從資料庫判斷使用者是否存在，並回傳使用者資訊
                var testUser = new User()
                {
                    Account = "art",
                    EmailAccount = "partypeopleland@gmail.com",
                    EmployeeNo = 123456,
                    UserName = "阿特"
                };
                return testUser;
            }
            // 查無使用者 (帳密錯誤)
            return null;
        }

        public ClaimsIdentity CreateClaimsIdentityByUser(User user)
        {
            return new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.EmployeeNo.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("Account", user.Account),
                    new Claim(ClaimTypes.Email, user.EmailAccount),
                });
        }
    }
}