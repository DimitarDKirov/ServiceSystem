namespace ServiceSystem.Web.Areas.Administration.Models.Categories
{
    using Data.Models;
    using Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class CategoryEditModel:IMapFrom<Category>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name="Min price")]
        [Range(typeof(decimal), "0.0", "10000000000.0", ErrorMessage ="Negative prices are not allowed")]
        public decimal MinPrice { get; set; }

        [Display(Name = "Max price")]
        [Range(typeof(decimal), "0.0", "10000000000.0", ErrorMessage = "Negative prices are not allowed")]
        public decimal MaxPrice { get; set; }
    }
}