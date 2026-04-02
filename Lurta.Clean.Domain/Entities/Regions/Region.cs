using Lurta.Clean.Domain.Entities.Orders;

namespace Lurta.Clean.Domain.Entities.Regions
{
    public sealed class Region
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Territory> Territories { get; set; }
    }
}
