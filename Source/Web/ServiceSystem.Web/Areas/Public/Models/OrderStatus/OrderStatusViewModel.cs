using System;
using System.ComponentModel.DataAnnotations;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Web.Areas.Public.Models.OrderStatus
{
    public class OrderStatusViewModel : IMapFrom<OrderModel>
    {
        public UnitViewModel Unit { get; set; }

        public CustomerViewModel Customer { get; set; }

        [Display(Name = "Order number")]
        public int Id { get; set; }

        [Display(Name = "Accepted at")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Warranty")]
        public WarrantyStatus WarrantyStatus { get; set; }

        public Status Status { get; set; }

        [Display(Name = "Repair finish date")]
        public DateTime? RepairEndDate { get; set; }

        [Display(Name = "Delivery date")]
        public DateTime? DeliverDate { get; set; }

        [Display(Name = "Problem")]
        [DataType(DataType.MultilineText)]
        public string ProblemDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string Solution { get; set; }

        [Display(Name = "Price")]
        public decimal LabourPrice { get; set; }
    }
}
