namespace ServiceSystem.Web.Areas.Administration.Models.Orders
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;

    public class OrdersModel : IMapFrom<Order>
    {
        [Display(Name = "Order number")]
        public int Id { get; set; }

        [Display(Name = "Accepted at")]
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "PublicKey")]
        public string OrderPublicId { get; set; }

        [Display(Name = "Warranty")]
        [EnumDataType(typeof(WarrantyStatus), ErrorMessage = "Warranty status is required")]
        public WarrantyStatus WarrantyStatus { get; set; }

        [Display(Name = "Warranty card")]
        [MaxLength(50)]
        public string WarrantyCard { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(OrdersModel), "CheckDate", ErrorMessage = "Date must be earlier than today")]
        public DateTime? WarrantyDate { get; set; }

        public Status Status { get; set; }

        [Display(Name = "Repair start date")]
        [DataType(DataType.Date)]
        public DateTime? RepairStartDate { get; set; }

        [Display(Name = "Repair finish date")]
        [DataType(DataType.Date)]
        public DateTime? RepairEndDate { get; set; }

        [Display(Name = "Delivery date")]
        [DataType(DataType.Date)]
        public DateTime? DeliverDate { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "Problem Description")]
        [DataType(DataType.MultilineText)]
        public string ProblemDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string Solution { get; set; }

        [Display(Name = "Price")]
        [CustomValidation(typeof(OrdersModel), "CheckPrice")]
        public decimal LabourPrice { get; set; }

        public static ValidationResult CheckDate(DateTime? warrantyDate, ValidationContext context)
        {
            if (warrantyDate == null)
            {
                return ValidationResult.Success;
            }

            if (warrantyDate > DateTime.Now)
            {
                return new ValidationResult("Warranty date must be earlier than today");
            }

            return ValidationResult.Success;
        }

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
