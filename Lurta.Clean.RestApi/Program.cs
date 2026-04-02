using LeadSoft.Common.Library.EnvUtils;
using Lurta.Clean.Infrastructure.IoC;
using Lurta.Clean.RestApi.Configurations;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Logging;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(o => o = new HttpLoggingOptions());

builder.Services.AddControllersConfiguration();
builder.Services.AddJwtAuthenticationConfiguration(builder.Configuration);

builder.Services.AddCultureConfiguration();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();

builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                        .AllowAnyMethod()
                                                                        .AllowAnyHeader()
                                                                        .WithExposedHeaders("Pagination")));

builder.Services.AddDependencyInjection();

if (!EnvUtil.IsProduction())
    builder.Services.AddSwaggerConfiguration();

WebApplication app = builder.Build();

IdentityModelEventSource.ShowPII = true;

if (!app.Environment.IsProduction())
{
    app.UseSwaggerConfiguration();
    app.UseDeveloperExceptionPage();
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.UseFileServer();
}

app.UseHttpLogging();
app.UseExceptionHandler(new ExceptionHandlerOptions
{
    ExceptionHandlingPath = "/api/Error",
    AllowStatusCode404Response = true
});

app.UseCors("AllowAll");
app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCompression();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("api/health");
});

app.Run();