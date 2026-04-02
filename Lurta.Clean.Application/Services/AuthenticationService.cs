using LeadSoft.Common.Library.EnvUtils;
using LeadSoft.Common.Library.Extensions;
using Lurta.Clean.Application.Contracts.Authentications;
using Lurta.Clean.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Lurta.Clean.Application.Services
{
    public partial class AuthenticationService(IConfiguration configuration) : IAuthenticationService
    {
        public async Task<DTOLoginResponse> LoginAsync(DTOLoginRequest dtoRequest)
        {
            return new(GenerateJwt(dtoRequest), dtoRequest.UserName);
        }

        private string GenerateJwt(DTOLoginRequest dtoRequest)
        {
            JwtSecurityToken jwt = new(configuration["Jwt:Issuer"],
                                        configuration["Jwt:Audience"],
                                        GetClaims(dtoRequest),
                                        expires: DateTime.UtcNow.AddMinutes(configuration["Jwt:ExpirationMinutes"].ToIntOrDefault(5)),
                                        signingCredentials: new(GetRsaPrivateSecKey(), SecurityAlgorithms.RsaSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private static RsaSecurityKey GetRsaPrivateSecKey()
        {
            RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(EnvUtil.Get(EnvUtil.JwtPrivateKey)), out _);

            return new(rsa);
        }

        private static IEnumerable<Claim> GetClaims(DTOLoginRequest dtoRequest)
        => [
                new (ClaimTypes.PrimarySid, "ProfileId"),
                new (ClaimTypes.Actor, "UserId"),
                new (ClaimTypes.Name, "UserName"),
                new (ClaimTypes.MobilePhone, "00000000000"),
                new (ClaimTypes.Email, dtoRequest.UserName),
                new (ClaimTypes.AuthenticationMethod, "GrantType"),
                new (ClaimTypes.Role, "Admin"),
                new (ClaimTypes.Expiration, "ExpireDateToken")
            ];
    }
}
