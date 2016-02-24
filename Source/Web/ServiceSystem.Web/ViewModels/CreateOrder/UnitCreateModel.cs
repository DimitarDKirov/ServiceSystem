namespace ServiceSystem.Web.ViewModels.CreateOrder
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class UnitCreateModel
    {
        [Required]
        public string Brand { get; set; }

        [MaxLength(20)]
        public string Model { get; set; }

        [MaxLength(50)]
        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
