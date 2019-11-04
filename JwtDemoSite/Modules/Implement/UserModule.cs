using JwtDemoSite.Models;

namespace JwtDemoSite.Modules.Implement
{
    public class UserModule : IUserModule
    {
        public User VerifyUser(string account, string password)
        {
            if (account.ToLower() == "art" && password == "1234")
            {
                // TODO: should verify user from db
                var testUser = new User()
                {
                    Account = "art",
                    EmailAccount = "partypeopleland@gmail.com",
                    EmployeeNo = 123456,
                    UserName = "阿特"
                };
                return testUser;
            }

            return null;
        }
    }
}