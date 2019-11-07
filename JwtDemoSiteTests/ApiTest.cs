using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using JwtDemoSite.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JwtDemoSiteTests
{
    /// <summary>
    /// API測試
    /// </summary>
    [TestClass]
    public class ApiTest
    {
        // 參考文章:https://dotblogs.com.tw/yc421206/2019/01/07/authentication_via_jwt-dotnet
        // Code：https://github.com/yaochangyu/sample.dotblog/blob/master/WebAPI/JWT/MsJwt/Client/UnitTest1.cs

        private const string Host = "http://localhost:17459";
        private static HttpClient _client;

        [TestInitialize]
        public void BeforeEach()
        {
            _client = new HttpClient { BaseAddress = new Uri(Host) };
        }

        [TestMethod]
        public void 無token執行回應unauthorized()
        {
            var queryUrl = "JWT/FeatureB";

            var queryResponse = _client.PostAsync(queryUrl, null).Result;
            Assert.AreEqual(HttpStatusCode.Unauthorized, queryResponse.StatusCode);
        }

        [TestMethod]
        public void 有token無權限執行回應unauthorized()
        {
            var queryUrl = "JWT/FeatureB";
            var token = GenerateTestToken();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var queryResponse = _client.PostAsync(queryUrl, null).Result;
            Assert.AreEqual(HttpStatusCode.Unauthorized, queryResponse.StatusCode);
        }

        [TestMethod]
        public void 有權限執行回應ok()
        {
            var queryUrl = "JWT/FeatureA";
            var token = GenerateTestToken();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var queryResponse = _client.PostAsync(queryUrl, null).Result;
            Assert.AreEqual(HttpStatusCode.OK, queryResponse.StatusCode);
        }

        private string GenerateTestToken()
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
            return token;
        }
    }
}