using System;
using System.Configuration;
using System.Linq;
using JwtDemoSite.Enums;
using JwtDemoSite.Extension;
using JwtDemoSite.Modules;
using JwtDemoSite.Modules.Implement;
using JwtDemoSite.Modules.Token;
using JwtDemoSite.Resource;

namespace JwtDemoSite.Factory
{
    public static class ModuleFactory
    {
        public static IUserModule GetUserModule() => new UserModule();

        public static ISystemAuthorityModule GetSystemAuthorityModule() => new SystemAuthorityModule();

        public static ITokenModule GetTokenModule()
        {
            var alg = ConfigurationManager.AppSettings["JWTProvider"].ParseEnum<TokenAlg>();

            var resource = JwtProviderResource.Strategies.FirstOrDefault(x => x.Alg == alg);
            if (resource == null) throw new NullReferenceException();
            return resource.Strategy;
        }
    }
}