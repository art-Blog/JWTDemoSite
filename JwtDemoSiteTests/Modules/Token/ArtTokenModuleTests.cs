using System;
using System.Security.Claims;
using JwtDemoSite.Models;
using JwtDemoSite.Modules.Token;
using JwtDemoSite.Modules.Token.Implement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JwtDemoSiteTests.Modules.Token
{
    [TestClass]
    public class ArtTokenTests
    {
        private ITokenModule _sut;

        [TestInitialize]
        public void BeforeEach()
        {
            _sut = new ArtTokenModule();
        }

        [TestMethod]
        public void GenerateToken()
        {
            var user = GetTestUser();
            var token = _sut.GenerateToken(user);
            Console.WriteLine(token);
        }

        [TestMethod]
        public void ParseToken()
        {
            var expected = GetTestUser();
            var token = GenerateTestToken();
            var principal = _sut.GetPrincipal(token);
            Assert.AreEqual(expected.UserName, principal.Identity.Name);
            Assert.AreEqual(expected.EmployeeNo.ToString(), principal.FindFirst(ClaimTypes.NameIdentifier).Value);
            Assert.AreEqual(expected.Account, principal.FindFirst("Account").Value);
            Assert.AreEqual(expected.EmailAccount, principal.FindFirst(ClaimTypes.Email).Value);
        }

        private string GenerateTestToken()
        {
            var user = GetTestUser();
            var token = _sut.GenerateToken(user);
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