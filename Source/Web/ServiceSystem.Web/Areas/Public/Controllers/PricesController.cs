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
            var prices = this.categoriesService
                .GetAll()
                .To<PricesViewModel>()
                .ToList();

            return this.View(prices);
        }
    }
}
