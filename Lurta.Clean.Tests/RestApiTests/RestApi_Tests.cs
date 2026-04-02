using LeadSoft.Common.Library.Constants;
using LeadSoft.Common.Library.Exceptions;
using Lurta.Clean.Tests.RestApiTests.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Xunit.Abstractions;

namespace Lurta.Clean.Tests.RestApiTests
{
    public partial class RestApi_Tests(ApiFactory factory, ITestOutputHelper output) : IClassFixture<ApiFactory>
    {
        #region [ Declarations & Constructor ]

        public static IEnumerable<object[]> Mocks
           => [
              ];

        #endregion

        [Fact]
        public async Task TestFactAsync()
        {
            bool x = true;

            Assert.True(x);
        }

        [Theory]
        [InlineData(1, HttpStatusCode.OK)]
        [InlineData(2, HttpStatusCode.BadRequest)]
        public async Task TestInlineControllerAsync(int id, HttpStatusCode expectedStatus)
        {
            using ControllerScope<Controller> controller = GetController<Controller>();

            if (factory.IsAuthorized())
                controller.Controller.HttpContext.Request.Headers.Add(Constant.Authorization, $"Bearer {factory.GetJwt()}");

            IActionResult controllerResult = await ExecuteControllerAsync(
                 async () =>
                 {
                     ActionResult<Object> actionResult = null;//await controller.Controller.GetAsync();
                     return actionResult.Result ?? new EmptyResult();
                 }
             );

            switch (expectedStatus)
            {
                case HttpStatusCode.OK:
                    {
                        OkObjectResult? ok = Assert.IsType<OkObjectResult>(controllerResult);
                        Object dto = Assert.IsType<Object>(ok.Value);
                        Assert.NotNull(dto);

                        output.WriteLine(Environment.NewLine);
                        break;
                    }
                default:
                    Assert.True(false, $"Status inesperado: {expectedStatus} / tipo real: {controllerResult.GetType().Name}");
                    break;
            }
        }

        [Theory]
        [MemberData(nameof(Mocks))]
        public async Task TestMockControllerAsync(object mock)
        {
            using ControllerScope<Controller> controller = GetController<Controller>();

            if (factory.IsAuthorized())
                controller.Controller.HttpContext.Request.Headers.Add(Constant.Authorization, $"Bearer {factory.GetJwt()}");

            IActionResult controllerResult = await ExecuteControllerAsync(
                 async () =>
                 {
                     ActionResult<Object> actionResult = null;//await controller.Controller.GetAsync();
                     return actionResult.Result ?? new EmptyResult();
                 }
             );

            OkObjectResult? ok = Assert.IsType<OkObjectResult>(controllerResult);
            Object dto = Assert.IsType<Object>(ok.Value);
            Assert.NotNull(dto);

            output.WriteLine(Environment.NewLine);
        }

        #region [ Private methods ]

        private ControllerScope<T> GetController<T>() where T : ControllerBase
        {
            IServiceScope scope = factory.Services.CreateScope();
            IServiceProvider provider = scope.ServiceProvider;

            T controller = ActivatorUtilities.CreateInstance<T>(provider);

            var httpContext = new DefaultHttpContext
            {
                RequestServices = provider
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
                {
                    RequestServices = provider,
                    Request =
                    {
                        Scheme = "https",
                        Host = new HostString("localhost", 7222),
                        PathBase = "/api"
                    }
                },
            };

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            controller.Url = new FakeUrlHelper(new ActionContext(httpContext, new RouteData(), new ActionDescriptor()), "https://localhost:7222/api");

            return new ControllerScope<T>(scope, controller);
        }

        public sealed class FakeUrlHelper(ActionContext actionContext, string baseUrl) : IUrlHelper
        {
            private readonly string _baseUrl = baseUrl.TrimEnd('/');
            public ActionContext ActionContext { get; } = actionContext;

            public string? Action(UrlActionContext actionContext)
                => $"{_baseUrl}/{actionContext.Action}".TrimEnd('/');

            public string? RouteUrl(UrlRouteContext routeContext)
                => $"{_baseUrl}/{routeContext.RouteName}".TrimEnd('/');

            public string? Content(string? contentPath)
                => string.IsNullOrWhiteSpace(contentPath) ? _baseUrl : $"{_baseUrl}/{contentPath.TrimStart('~', '/')}";

            public bool IsLocalUrl(string? url) => true;

            public string? Link(string? routeName, object? values)
                => string.IsNullOrWhiteSpace(routeName) ? _baseUrl : $"{_baseUrl}/{routeName}".TrimEnd('/');
        }

        private static async Task<IActionResult> ExecuteControllerAsync(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (BadRequestAppException e)
            {
                return new BadRequestObjectResult(e.Messages.FirstOrDefault());
            }
            catch (NotFoundAppException e)
            {
                return new NotFoundObjectResult(e.Messages.FirstOrDefault());
            }
            catch (ConflictAppException e)
            {
                return new ConflictObjectResult(e.Messages.FirstOrDefault());
            }
            catch (UnauthorizedAppException e)
            {
                return new UnauthorizedObjectResult(e.Messages.FirstOrDefault());
            }
            catch (ForbiddenAppException)
            {
                return new ForbidResult();
            }
            catch (AppException e)
            {
                return new ObjectResult(e.Messages.FirstOrDefault());
            }
        }

        #endregion
    }
}
