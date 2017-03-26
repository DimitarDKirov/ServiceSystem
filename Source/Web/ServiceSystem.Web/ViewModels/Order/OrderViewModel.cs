using System;
using System.ComponentModel.DataAnnotations;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Data.Models;
using AutoMapper;

namespace ServiceSystem.Web.ViewModels.Order
{
    public class OrderViewModel : IMapFrom<OrderModel>, IMapTo<OrderUpdateModel>
    {
        public UnitViewModel Unit { get; set; }

        public CustomerViewCreateModel Customer { get; set; }

        [Display(Name = "Order number")]
        public int Id { get; set; }

        [Display(Name = "Accepted at")]
        public DateTime CreatedOn { get; set; }

        public string OrderPublicId { get; set; }

        [Display(Name = "Warranty")]
        [EnumDataType(typeof(WarrantyStatus), ErrorMessage = "Warranty status is required")]
        public WarrantyStatus WarrantyStatus { get; set; }

        public string WarrantyCard { get; set; }

        [Required]
        [Display(Name = "Problem")]
        [DataType(DataType.MultilineText)]
        public string ProblemDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string Solution { get; set; }

        [EnumDataType(typeof(Status), ErrorMessage = "Status is required")]
        public Status Status { get; set; }

        [Display(Name = "Repair start date")]
        public DateTime? RepairStartDate { get; set; }

        [Display(Name = "Repair finish date")]
        public DateTime? RepairEndDate { get; set; }

        [Display(Name = "Delivery date")]
        public DateTime? DeliverDate { get; set; }

        [Display(Name = "Price")]
        [CustomValidation(typeof(OrderViewModel), "CheckPrice")]
        public decimal LabourPrice { get; set; }

        [Display(Name = "Engineer")]
        public string User { get; set; }

        public bool IsEditable { get; set; }

        public static ValidationResult CheckPrice(decimal price, ValidationContext context)
        {
            if (price < 0)
            {
                return new ValidationResult("Price can not be negative");
            }

            return ValidationResult.Success;
        }
    }
}
