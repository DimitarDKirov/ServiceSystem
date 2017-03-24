using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Services.Data.Models
{
    public class CategoryModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }
    }
}
