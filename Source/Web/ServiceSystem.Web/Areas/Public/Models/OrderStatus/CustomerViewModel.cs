namespace ServiceSystem.Web.Areas.Public.Models.OrderStatus
{
    using ServiceSystem.Data.Models;
    using ServiceSystem.Web.Infrastructure.Mapping;

    public class CustomerViewModel : IMapFrom<Customer>
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
