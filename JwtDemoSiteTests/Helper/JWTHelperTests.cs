using System;
using System.Security.Claims;
using JwtDemoSite.Helper;
using JwtDemoSite.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JwtDemoSiteTests.Helper
{
    [TestClass]
    public class JWTHelperTests
    {
        [TestMethod]
        public void GenerateToken()
        {
            var user = GetTestUser();
            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.EmployeeNo.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("Account", user.UserName),
                    new Claim(ClaimTypes.Email, user.EmailAccount)
                });
            var token = JWTHelper.GenerateToken(identity);
            Console.WriteLine(token);
        }

        [TestMethod]
        public void ParseToken()
        {
            var expected = GetTestUser();
            var token = GenerateTestToken();
            var principal = JWTHelper.GetPrincipal(token);
            Assert.AreEqual(expected.UserName, principal.Identity.Name);
            Assert.AreEqual(expected.EmployeeNo.ToString(), principal.FindFirst(ClaimTypes.NameIdentifier).Value);
            Assert.AreEqual(expected.Account, principal.FindFirst("Account").Value);
            Assert.AreEqual(expected.EmailAccount, principal.FindFirst(ClaimTypes.Email).Value);
        }

        private string GenerateTestToken()
        {
            var user = GetTestUser();
            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.EmployeeNo.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("Account", user.Account),
                    new Claim(ClaimTypes.Email, user.EmailAccount)
                });
            var token = JWTHelper.GenerateToken(identity);
            return token;
        }

        private User GetTestUser()
        {
            return new User
            {
                Account = "spiderman",
                EmailAccount = "spiderman@email.com",
                UserName = "蜘蛛人",
                EmployeeNo = 123456
            };
        }
    }
}