using System;
using System.ComponentModel.DataAnnotations;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Web.ViewModels.Order;

namespace ServiceSystem.Web.ViewModels
{
    public class CommonDetailsViewModel : IMapFrom<OrderModel>
    {
        public UnitViewModel Unit { get; set; }

        public CustomerViewCreateModel Customer { get; set; }

        [Display(Name = "Order number")]
        public int Id { get; set; }

        [Display(Name = "Accepted at")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Warranty")]
        public WarrantyStatus WarrantyStatus { get; set; }

        public Status Status { get; set; }

        [Display(Name = "Repair start date")]
        public DateTime? RepairStartDate { get; set; }

        [Display(Name = "Repair finish date")]
        public DateTime? RepairEndDate { get; set; }

        [Display(Name = "Delivery date")]
        public DateTime? DeliverDate { get; set; }
    }
}
