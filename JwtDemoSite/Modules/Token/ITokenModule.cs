using System.Security.Claims;

namespace JwtDemoSite.Modules.Token
{
    public interface ITokenModule
    {
        /// <summary>
        /// 產生Token
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="expireMinutes">有效時間</param>
        /// <returns></returns>
        string GenerateToken(object identity, int expireMinutes = 20);

        /// <summary>
        /// 解析Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        ClaimsPrincipal GetPrincipal(string token);
    }
}