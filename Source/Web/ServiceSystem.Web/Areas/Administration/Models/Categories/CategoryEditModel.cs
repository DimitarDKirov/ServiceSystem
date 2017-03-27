using System.ComponentModel.DataAnnotations;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Web.Areas.Administration.Models.Categories
{
    public class CategoryEditModel : IMapFrom<CategoryModel>, IMapTo<CategoryModel>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Min price")]
        [Range(typeof(decimal), "0.0", "10000000000.0", ErrorMessage = "Negative prices are not allowed")]
        public decimal MinPrice { get; set; }

        [Display(Name = "Max price")]
        [Range(typeof(decimal), "0.0", "10000000000.0", ErrorMessage = "Negative prices are not allowed")]
        public decimal MaxPrice { get; set; }
    }
}
