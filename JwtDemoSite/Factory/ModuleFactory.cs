using JwtDemoSite.Modules;
using JwtDemoSite.Modules.Implement;

namespace JwtDemoSite.Factory
{
    public static class ModuleFactory
    {
        public static IUserModule GetUserModule() => new UserModule();

        public static ISystemAuthorityModule GetSystemAuthorityModule() => new SystemAuthorityModule();
    }
}