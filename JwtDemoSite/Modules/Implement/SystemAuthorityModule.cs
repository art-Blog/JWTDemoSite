using System.Linq;

namespace JwtDemoSite.Modules.Implement
{
    public class SystemAuthorityModule : ISystemAuthorityModule
    {
        public bool ValidateUserFunction(int employeeNo, string[] urls)
        {
            // 應該從資料庫中，依據使用者員工代碼，取得該員工的權限
            var functionList = new[] { "/JWT/FeatureA" };

            return functionList.Intersect(urls).Any();
        }
    }
}