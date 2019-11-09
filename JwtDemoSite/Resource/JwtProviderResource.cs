using System.Collections.Generic;
using JwtDemoSite.Enums;
using JwtDemoSite.Modules;
using JwtDemoSite.Modules.Implement;
using JwtDemoSite.Modules.Token;
using JwtDemoSite.Modules.Token.Implement;

namespace JwtDemoSite.Resource
{
    /// <summary>
    /// JWT 演算法資源
    /// </summary>
    internal class JwtProviderResource
    {
        internal TokenAlg Alg { get; }
        internal ITokenModule Strategy { get; }

        public JwtProviderResource(TokenAlg alg, ITokenModule module)
        {
            this.Alg = alg;
            this.Strategy = module;
        }

        private static List<JwtProviderResource> _strategies;

        internal static List<JwtProviderResource> Strategies
        {
            get
            {
                if (_strategies == null)
                {
                    GetStrategies();
                }
                return _strategies;
            }
        }

        private static void GetStrategies()
        {
            _strategies = new List<JwtProviderResource>
            {
                new JwtProviderResource(TokenAlg.Hmac, new HmacTokenModule()),
                new JwtProviderResource(TokenAlg.Rsa, new RsaTokenModule())
            };
        }
    }
}