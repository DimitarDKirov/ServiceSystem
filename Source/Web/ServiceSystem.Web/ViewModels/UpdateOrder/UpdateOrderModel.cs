namespace ServiceSystem.Web.ViewModels.UpdateOrder
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UpdateOrderModel : IMapFrom<Order>
    {
        [Display(Name = "Order number")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Problem")]
        [DataType(DataType.MultilineText)]
        public string ProblemDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string Solution { get; set; }

        [Display(Name = "Price")]
        [CustomValidation(typeof(UpdateOrderModel), "CheckPrice")]
        public decimal LabourPrice { get; set; }

        [EnumDataType(typeof(Status), ErrorMessage = "Status is required")]
        public Status Status { get; set; }

        [Display(Name = "Warranty")]
        [EnumDataType(typeof(WarrantyStatus), ErrorMessage = "Warranty status is required")]
        public WarrantyStatus WarrantyStatus { get; set; }

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
