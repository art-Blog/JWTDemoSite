using System;
using System.Security.Claims;
using System.Web.Mvc;
using JwtDemoSite.Factory;
using JwtDemoSite.Filters;
using JwtDemoSite.Helper;
using JwtDemoSite.Models;
using JwtDemoSite.Modules;

namespace JwtDemoSite.Controllers
{
    public class JWTController : Controller
    {
        /// <summary>
        /// 使用者Module
        /// </summary>
        private readonly Lazy<IUserModule> _userModule = new Lazy<IUserModule>(ModuleFactory.GetUserModule);

        protected IUserModule UserModule => this._userModule.Value;

        /// <summary>
        /// 取得JWT Token
        /// </summary>
        /// <param name="loginForm">The login form.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetToken(LoginForm loginForm)
        {
            // 輸入驗證
            if (string.IsNullOrEmpty(loginForm.Account) || string.IsNullOrEmpty(loginForm.Password))
            {
                return Json(new APIResult() { Message = "請輸入帳號密碼", IsSuccess = false });
            }

            // 驗證使用者
            var user = UserModule.VerifyUser(loginForm.Account, loginForm.Password);
            if (user == null)
            {
                return Json(new APIResult() { Message = "帳號密碼錯誤", IsSuccess = false });
            }

            // 產生token
            var identity = new ClaimsIdentity(
               new[]
                 {
                      new Claim(ClaimTypes.NameIdentifier, user.EmployeeNo.ToString()),
                      new Claim(ClaimTypes.Name, user.UserName),
                      new Claim("Account", user.Account),
                      new Claim(ClaimTypes.Email, user.EmailAccount),
                 });
            var token = JWTHelper.GenerateToken(identity);

            return Json(new APIResult() { Data = token, IsSuccess = true, Message = string.Empty });
        }

        /// <summary>
        /// Features a.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [JWTAuth]
        public JsonResult FeatureA()
        {
            return Json(new APIResult() { IsSuccess = true, Message = "允許執行" });
        }

        /// <summary>
        /// Features the b.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [JWTAuth]
        public JsonResult FeatureB()
        {
            return Json(new APIResult() { IsSuccess = true, Message = "不允許執行" });
        }
    }
}