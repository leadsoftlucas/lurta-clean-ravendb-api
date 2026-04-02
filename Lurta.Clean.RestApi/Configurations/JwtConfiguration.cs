using LeadSoft.Common.Library.EnvUtils;
using LeadSoft.Common.Library.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Lurta.Clean.RestApi.Configurations
{
    public static class JwtConfiguration
    {
        public static void AddJwtAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtconfig =>
            {
                jwtconfig.RequireHttpsMetadata = false;
                jwtconfig.SaveToken = true;
                jwtconfig.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = GetRsaPublicSecKey(),

                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],

                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        private static RsaSecurityKey GetRsaPublicSecKey()
        {
            if (EnvUtil.Get(EnvUtil.JwtPublicKey).IsNothing())
                return null;

            RSA rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(EnvUtil.Get(EnvUtil.JwtPublicKey)), out _);

            return new(rsa);
        }
    }
}
