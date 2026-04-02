using Lurta.Clean.Domain.Entities.Orders;
using Lurta.Clean.Domain.ValueObjects;

namespace Lurta.Clean.Domain.Entities.Suppliers
{
    public sealed class Supplier
    {
        public string Id { get; set; }
        public Contact Contact { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string HomePage { get; set; }
    }
}
