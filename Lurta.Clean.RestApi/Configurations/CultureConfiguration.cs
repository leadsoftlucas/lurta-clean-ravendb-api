using System.Globalization;

namespace Lurta.Clean.RestApi.Configurations
{
    public static class CultureConfiguration
    {
        public static void AddCultureConfiguration(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;
        }
    }
}
