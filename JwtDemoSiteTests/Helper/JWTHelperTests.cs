using System;
using System.Security.Claims;
using JwtDemoSite.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JwtDemoSiteTests.Helper
{
    [TestClass()]
    public class JWTHelperTests
    {
        [TestMethod()]
        public void GenerateToken()
        {
            var identity = new ClaimsIdentity(
            new[]
              {
                      new Claim(ClaimTypes.NameIdentifier, "123456"),
                      new Claim(ClaimTypes.Name, "蜘蛛人"),
                      new Claim("Account", "spiderman"),
                      new Claim(ClaimTypes.Email, "spiderman@email.com"),
              });
            var token = JWTHelper.GenerateToken(identity);
            Console.WriteLine(token);
        }
    }
}