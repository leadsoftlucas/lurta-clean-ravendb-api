using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lurta.Clean.RestApi.Configurations
{
    public static class ControllersConfiguration
    {
        public static void AddControllersConfiguration(this IServiceCollection services)
        {
            services.AddControllers(static options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
                options.ReturnHttpNotAcceptable = true;
                options.EnableEndpointRouting = true;
                options.RequireHttpsPermanent = true;
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            }).AddJsonOptions(static jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                jsonOptions.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                jsonOptions.JsonSerializerOptions.IgnoreReadOnlyFields = true;
                jsonOptions.JsonSerializerOptions.AllowTrailingCommas = true;
                jsonOptions.JsonSerializerOptions.IncludeFields = true;
                jsonOptions.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                jsonOptions.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
            }).AddNewtonsoftJson(static newtonsoft =>
            {
                newtonsoft.SerializerSettings.Converters.Add(new StringEnumConverter());
                newtonsoft.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    IgnoreSerializableAttribute = false,
                    SerializeCompilerGeneratedMembers = true,
                    IgnoreIsSpecifiedMembers = false,
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
                newtonsoft.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                newtonsoft.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                newtonsoft.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            });
            services.AddResponseCompression();
        }
    }
}
