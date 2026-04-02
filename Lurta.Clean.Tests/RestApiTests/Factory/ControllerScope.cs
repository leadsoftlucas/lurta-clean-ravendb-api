using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Lurta.Clean.Tests.RestApiTests.Factory
{
    public sealed class ControllerScope<T>(IServiceScope scope, T controller) : IDisposable where T : ControllerBase
    {
        public T Controller { get; } = controller;

        public void Dispose() => scope.Dispose();
    }
}
