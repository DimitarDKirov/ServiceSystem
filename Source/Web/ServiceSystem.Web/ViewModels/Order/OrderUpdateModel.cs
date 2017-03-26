using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Web.ViewModels.Order
{
    public class OrderUpdateModel : IMapTo<OrderModel>, IMapTo<OrderViewModel>
    {
        public int Id { get; set; }

        public string ProblemDescription { get; set; }

        public string Solution { get; set; }

        public decimal LabourPrice { get; set; }

        public Status Status { get; set; }

        public WarrantyStatus WarrantyStatus { get; set; }
    }
}