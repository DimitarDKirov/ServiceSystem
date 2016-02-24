namespace ServiceSystem.Web.Areas.Administration.Models.Categories
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;

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
