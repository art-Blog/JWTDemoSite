namespace JwtDemoSite.Modules
{
    public interface ISystemAuthorityModule
    {
        /// <summary>
        /// 驗證使用者功能權限
        /// </summary>
        /// <param name="employeeNo">員工編號</param>
        /// <param name="urls">要被驗證的路由 (ex:/JWT/FeatureA)</param>
        /// <returns>True:有權限 / False:無權限</returns>
        bool ValidateUserFunction(int employeeNo, string[] urls);
    }
}