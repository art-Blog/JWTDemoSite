using System.Linq;

namespace JwtDemoSite.Modules.Implement
{
    public class SystemAuthorityModule : ISystemAuthorityModule
    {
        public bool ValidateUserFunction(int employeeNo, string[] urls)
        {
            // TODO:get employee's function list from db
            var functionList = new[] { "/JWT/FeatureA" };
            return functionList.Intersect(urls).Any();
        }
    }
}