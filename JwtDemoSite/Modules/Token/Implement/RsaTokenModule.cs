using System;
using System.Security.Claims;

namespace JwtDemoSite.Modules.Token.Implement
{
    internal class RsaTokenModule : ITokenModule
    {
        public string GenerateToken(ClaimsIdentity identity, int expireMinutes = 20)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            throw new NotImplementedException();
        }
    }
}