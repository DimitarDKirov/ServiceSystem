namespace ServiceSystem.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CommonDetailsViewModel : IMapFrom<Order>
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

        [Display(Name = "Repair start date")]
        public DateTime? RepairStartDate { get; set; }

        [Display(Name = "Repair finish date")]
        public DateTime? RepairEndDate { get; set; }

        [Display(Name = "Delivery date")]
        public DateTime? DeliverDate { get; set; }
    }
}
