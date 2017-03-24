using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Services.Data.Models
{
    public class UnitModel : IMapFrom<Unit>
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public string Brand { get; set; }
    }
}
