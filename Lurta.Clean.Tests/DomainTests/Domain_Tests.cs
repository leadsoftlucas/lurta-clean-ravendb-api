using LeadSoft.Common.Library.Extensions;
using Lurta.Clean.Tests.DomainTests.Fixtures;
using System.Security.Cryptography;
using Xunit.Abstractions;

namespace Lurta.Clean.Tests.DomainTests
{
    public class Domain_Tests(AppSettingsFixture fx, ITestOutputHelper output) : IClassFixture<AppSettingsFixture>
    {
        public static IEnumerable<object[]> Mocks
           => [
              ];

        [Fact]
        public async Task CreateJwsAsync()
        {
            RSA rsa = RSA.Create();

            string x = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            string y = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

            output.WriteLine($"Public: {x}");
            output.WriteLine($"Private: {y}");
        }

        [Fact]
        public async Task TestFactAsync()
        {
            bool x = true;

            Assert.True(x);
            output.WriteLine($"{x}");
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task TestInlineAsync(bool x)
        {
            Assert.True(x);
            output.WriteLine($"{x}");
        }

        [Theory]
        [MemberData(nameof(Mocks))]
        public async Task TestMocksAsync(object mock)
        {
            Assert.NotNull(mock);
            output.WriteLine(mock.ToJson().FormatJson());
        }
    }
}
