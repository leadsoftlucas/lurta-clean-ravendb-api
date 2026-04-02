using LeadSoft.Common.Library.EnvUtils;
using LeadSoft.Common.Library.Extensions;
using Lurta.Clean.Application.Contracts.Authentications;
using Lurta.Clean.Application.Services.Interfaces;
using Lurta.Clean.Domain.Entities.Usuarios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Raven.Client.Documents;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Lurta.Clean.Application.Services
{
    public partial class AuthenticationService(IDocumentStore ravenDB, IConfiguration configuration) : IAuthenticationService
    {
        public async Task<DTOLoginResponse> LoginAsync(DTOLoginRequest dtoRequest)
        {
            using var session = ravenDB.OpenAsyncSession();

            Usuario usuario = await session.Query<Usuario>().FirstOrDefaultAsync(u => u.Username.Equals(dtoRequest.Username));

            if (usuario is null || !usuario.ValidatePassword(dtoRequest.Password))
                return null;

            return new(GenerateJwt(usuario), usuario.Username);
        }

        private string GenerateJwt(Usuario usuario)
        {
            JwtSecurityToken jwt = new(configuration["Jwt:Issuer"],
                                        configuration["Jwt:Audience"],
                                        GetClaims(usuario),
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

        private static IEnumerable<Claim> GetClaims(Usuario usuario)
        => [
                new (ClaimTypes.PrimarySid, usuario.Id),
                new (ClaimTypes.Actor, "UserId"),
                new (ClaimTypes.Name, "UserName"),
                new (ClaimTypes.MobilePhone, "00000000000"),
                new (ClaimTypes.Email, usuario.Username),
                new (ClaimTypes.AuthenticationMethod, "GrantType"),
                new (ClaimTypes.Role, "Admin"),
                new (ClaimTypes.Expiration, "ExpireDateToken")
            ];
    }
}
