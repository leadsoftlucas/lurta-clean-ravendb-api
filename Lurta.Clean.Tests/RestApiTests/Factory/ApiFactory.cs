using LeadSoft.Common.Library.Extensions;
using LeadSoft.Common.Library.Extensions.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.IdentityModel.Tokens.Jwt;

namespace Lurta.Clean.Tests.RestApiTests.Factory
{
    public class ApiFactory : WebApplicationFactory<Program>
    {
        private string _Jwt { get; set; } = string.Empty;
        private string _JwtRefreshToken { get; set; } = string.Empty;

        public string SetJwt(string jwt)
        {
            _Jwt = jwt;

            if (jwt.IsSomething())
                _JwtRefreshToken = new JwtSecurityToken(_Jwt).RetrieveClaimField("RefreshToken");
            else
                _JwtRefreshToken = string.Empty;

            return GetJwt();
        }

        public string GetJwt() => _Jwt;
        public string GetJwtRefreshToken() => _JwtRefreshToken;
        public string ClearJwt() => SetJwt(string.Empty);
        public bool IsAuthorized() => _Jwt.IsSomething();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");
        }
    }
}
