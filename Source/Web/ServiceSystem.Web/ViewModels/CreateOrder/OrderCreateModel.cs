namespace ServiceSystem.Web.ViewModels.CreateOrder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ServiceSystem.Data.Models;

    public class OrderCreateModel
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

        public CustomerCreateModel Customer { get; set; }

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
    }
}
