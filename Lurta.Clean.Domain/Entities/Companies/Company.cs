using Lurta.Clean.Domain.ValueObjects;
using Raven.Client.Documents.Session.TimeSeries;

namespace Lurta.Clean.Domain.Entities.Companies
{
    public sealed class Company
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public sealed class StockPrice
        {
            [TimeSeriesValue(0)] public double Open { get; set; }
            [TimeSeriesValue(1)] public double Close { get; set; }
            [TimeSeriesValue(2)] public double High { get; set; }
            [TimeSeriesValue(3)] public double Low { get; set; }
            [TimeSeriesValue(4)] public double Volume { get; set; }
        }
    }
}
