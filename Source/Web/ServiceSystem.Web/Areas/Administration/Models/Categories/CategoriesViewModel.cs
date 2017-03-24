using System.ComponentModel.DataAnnotations;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Web.Areas.Administration.Models.Categories
{
    public class CategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Price from")]
        public decimal MinPrice { get; set; }

        [Display(Name = "Price to")]
        public decimal MaxPrice { get; set; }
    }
}
