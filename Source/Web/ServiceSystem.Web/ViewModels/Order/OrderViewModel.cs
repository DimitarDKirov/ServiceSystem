using System;
using System.ComponentModel.DataAnnotations;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Data.Models;

namespace ServiceSystem.Web.ViewModels.Order
{
    public class OrderViewModel : IMapFrom<OrderModel>
    {
        public UnitViewModel Unit { get; set; }

        public CustomerViewCreateModel Customer { get; set; }

        [Display(Name = "Order number")]
        public int Id { get; set; }

        [Display(Name = "Accepted at")]
        public DateTime CreatedOn { get; set; }

        public string OrderPublicId { get; set; }

        [Display(Name = "Warranty")]
        public string WarrantyStatus { get; set; }

        public string WarrantyCard { get; set; }

        [Display(Name = "Problem")]
        [DataType(DataType.MultilineText)]
        public string ProblemDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string Solution { get; set; }

        public Status Status { get; set; }

        [Display(Name = "Repair start date")]
        public DateTime? RepairStartDate { get; set; }

        [Display(Name = "Repair finish date")]
        public DateTime? RepairEndDate { get; set; }

        [Display(Name = "Delivery date")]
        public DateTime? DeliverDate { get; set; }

        [Display(Name = "Price")]
        public decimal LabourPrice { get; set; }

        [Display(Name = "Engineer")]
        public string User { get; set; }

        public bool IsEditable { get; set; }
    }
}
