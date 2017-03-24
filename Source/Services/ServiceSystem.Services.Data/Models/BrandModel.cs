using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Services.Data.Models
{
    public class BrandModel : IMapFrom<Brand>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
