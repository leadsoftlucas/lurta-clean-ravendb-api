using LeadSoft.Common.Library.Constants;
using LeadSoft.Common.Library.EnvUtils;
using LeadSoft.Common.Library.Extensions;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace Lurta.Clean.RestApi.Configurations
{
    public static class SwaggerConfiguration
    {
        private static readonly string _SwaggerTitle = $"Swagger Docs | Lurta Clean Rest Api | Brasil | {EnvUtil.Get(EnvUtil.AspNet)} environment";

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddOpenApi();
            services.AddSwaggerGen(
                 c =>
                 {
                     c.SwaggerDoc("v1", GetOpenApiInfo());
                     c.AddSecurityDefinition(Constant.Authorization, GetAuthorizationScheme());
                     c.AddSecurityRequirement(document => GetAuthorizationRequirement(document));
                     c.EnableAnnotations();
                     c.CustomSchemaIds(s => s.FullName.Replace("+", "."));

                     if (File.Exists(Path.Combine(AppContext.BaseDirectory, $"{PlatformServices.Default.Application.ApplicationName}.xml")))
                         c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{PlatformServices.Default.Application.ApplicationName}.xml"));
                 }
             );
        }

        public static void UseSwaggerConfiguration(this WebApplication app)
        {
            ArgumentNullException.ThrowIfNull(app);

            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", _SwaggerTitle);
                c.DocExpansion(DocExpansion.None);
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Example);
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableTryItOutByDefault();
                c.EnableValidator();
                c.ShowCommonExtensions();
            });
        }

        private static OpenApiInfo GetOpenApiInfo()
        => new()
        {
            Title = _SwaggerTitle,
            Version = "v1",
            Description = Assembly.GetExecutingAssembly().GetEmbeddedResourceContent($"README.md", true),
            Contact = new OpenApiContact
            {
                Name = "Lucas Tavares | RavenDB | LeadSoft",
                Email = "eu@lucasrtavares.com.br;lucas.tavares@ravendb.net",
                Url = new Uri("https://www.linkedin.com/in/lucasrtavares/")
            }
        };

        private static OpenApiSecurityScheme GetAuthorizationScheme()
        => new()
        {
            Name = Constant.Authorization,
            Description = "Informe o token JWT no header Authorization (Bearer {token}).",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        };

        private static OpenApiSecurityRequirement GetAuthorizationRequirement(OpenApiDocument document)
          => new()
          {
              [new OpenApiSecuritySchemeReference(Constant.Authorization, document)] = []
          };
    }
}
