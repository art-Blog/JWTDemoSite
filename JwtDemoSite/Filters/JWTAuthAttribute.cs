using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using JwtDemoSite.Factory;
using JwtDemoSite.Helper;
using JwtDemoSite.Modules;

namespace JwtDemoSite.Filters
{
    public class JWTAuthAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 系統權限Module
        /// </summary>
        private ISystemAuthorityModule _systemAuthorityModule;

        protected ISystemAuthorityModule SystemAuthorityModule
        {
            get => this._systemAuthorityModule ?? (this._systemAuthorityModule = ModuleFactory.GetSystemAuthorityModule());
            set => this._systemAuthorityModule = value;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var principal = ParseAuthorizeHeader(httpContext);
            if (principal == null) return false;

            // 取得員工編號
            var employeeNo = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (employeeNo == 0) return false;

            // 驗證權限
            var controller = Convert.ToString(httpContext.Request.RequestContext.RouteData.Values["controller"]);
            var action = Convert.ToString(httpContext.Request.RequestContext.RouteData.Values["action"]);
            var urls = new[] { $"/{controller}", $"/{controller}/{action}" };
            var result = this.SystemAuthorityModule.ValidateUserFunction(employeeNo, urls);

            return result;
        }

        /// <summary>
        /// 解析header取得principal
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns></returns>
        private ClaimsPrincipal ParseAuthorizeHeader(HttpContextBase httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token)) return null;

            token = token.Replace("Bearer ", string.Empty);
            var principal = JWTHelper.GetPrincipal(token);

            return principal;
        }
    }
}