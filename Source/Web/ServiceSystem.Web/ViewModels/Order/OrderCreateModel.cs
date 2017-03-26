using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.DateProvider;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Web.ViewModels.Order
{
    public class OrderCreateModel : IMapFrom<OrderModel>, IMapTo<OrderModel>
    {
        [Display(Name = "Warranty")]
        [EnumDataType(typeof(WarrantyStatus), ErrorMessage = "Warranty status is required")]
        public WarrantyStatus WarrantyStatus { get; set; }

        [MaxLength(50)]
        [Display(Name = "Warranty card Number")]
        public string WarrantyCard { get; set; }

        [Display(Name = "Warranty card date")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(OrderCreateModel), "CheckDate", ErrorMessage = "Date must be earlier than today")]
        public DateTime? WarrantyDate { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "Problem Description")]
        public string ProblemDescription { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public UnitCreateModel Unit { get; set; }

        public CustomerViewCreateModel Customer { get; set; }

        public static ValidationResult CheckDate(DateTime? warrantyDate, ValidationContext context)
        {
            if (warrantyDate == null)
            {
                return ValidationResult.Success;
            }

            if (warrantyDate > DateTimeProvider.Current.UtcNow)
            {
                return new ValidationResult("Warranty date must be earlier than today");
            }

            return ValidationResult.Success;
        }
    }
}
