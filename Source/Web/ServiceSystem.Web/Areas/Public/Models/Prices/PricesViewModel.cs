namespace ServiceSystem.Web.Areas.Public.Models.Prices
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class PricesViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }
    }
}
