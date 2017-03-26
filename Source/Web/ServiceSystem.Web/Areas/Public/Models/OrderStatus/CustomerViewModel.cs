using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Web.Areas.Public.Models.OrderStatus
{
    public class CustomerViewModel : IMapFrom<CustomerModel>
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
