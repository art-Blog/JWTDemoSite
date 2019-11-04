using JwtDemoSite.Models;

namespace JwtDemoSite.Modules
{
    public interface IUserModule
    {
        /// <summary>
        /// 驗證使用者
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        User VerifyUser(string account, string password);
    }
}