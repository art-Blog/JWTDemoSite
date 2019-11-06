using System;
using System.Security.Claims;
using JwtDemoSite.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JwtDemoSite.Test.Helper
{
    [TestClass]
    public class JWTHelperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var identity = new ClaimsIdentity(
              new[]
                {
                      new Claim(ClaimTypes.NameIdentifier, "123456"),
                      new Claim(ClaimTypes.Name, "ªü¯S"),
                      new Claim("Account", "art"),
                      new Claim(ClaimTypes.Email, "partypeopleland@gmail.com"),
                });
            var expires = new DateTime(2019, 11, 6, 18, 0, 0).AddHours(-8);
            var token = JWTHelper.GenerateToken(identity, expires);
            Console.WriteLine(token);
        }
    }
}