using System.ComponentModel.DataAnnotations;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Web.Areas.Administration.Models.Categories
{
    public class CategoriesViewModel : IMapFrom<CategoryModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Price from")]
        public decimal MinPrice { get; set; }

        [Display(Name = "Price to")]
        public decimal MaxPrice { get; set; }
    }
}
