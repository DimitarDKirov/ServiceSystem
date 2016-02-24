namespace ServiceSystem.Web.Areas.Public.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Models.Prices;
    using Services.Data;
    using Web.Controllers;

    public class PricesController : BaseController
    {
        private ICategoriesService categoriesService;

        public PricesController(ICategoriesService service)
        {
            this.categoriesService = service;
        }

        public ActionResult Index()
        {
            var prices = this.Cache.Get(
                "PricesPublic",
                () => this.categoriesService
                .GetAll()
                .To<PricesViewModel>()
                .ToList(),
                24 * 60 * 60);

            return this.View(prices);
        }
    }
}
