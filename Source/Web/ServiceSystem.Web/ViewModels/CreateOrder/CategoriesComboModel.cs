namespace ServiceSystem.Web.ViewModels.CreateOrder
{
    using ServiceSystem.Data.Models;
    using ServiceSystem.Web.Infrastructure.Mapping;

    public class CategoriesComboModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
