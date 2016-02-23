namespace ServiceSystem.Web.ViewModels.OrdersGrid
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;

    public class OrdersGridViewModel : IMapFrom<Order>
    {
        [Display(Name = "Order number")]
        public int Id { get; set; }

        [Display(Name = "Accepted at")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "PublicKey")]
        public string OrderPublicId { get; set; }

        [Display(Name = "Warranty")]
        public WarrantyStatus WarrantyStatus { get; set; }

        [Display(Name = "Warranty card")]
        public string WarrantyCard { get; set; }

        public DateTime? WarrantyDate { get; set; }

        public Status Status { get; set; }

        [Display(Name = "Repair start date")]
        public DateTime? RepairStartDate { get; set; }

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
