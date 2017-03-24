using System;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Services.Data.Models
{
    public class OrderModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public string OrderPublicId { get; set; }

        public DateTime? RepairStartDate { get; set; }

        public DateTime? RepairEndDate { get; set; }

        public DateTime? DeliverDate { get; set; }

        public string UserId { get; set; }

        public string ProblemDescription { get; set; }

        public string Solution { get; set; }

        public WarrantyStatus WarrantyStatus { get; set; }

        public string WarrantyCard { get; set; }

        public DateTime? WarrantyDate { get; set; }

        public decimal LabourPrice { get; set; }
    }
}
