using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Services.Data.Models
{
    public class CustomerModel : IMapFrom<Customer>, IMapTo<Customer>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
