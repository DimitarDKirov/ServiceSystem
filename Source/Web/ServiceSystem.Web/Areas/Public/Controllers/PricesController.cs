using System.Collections.Generic;
using System.Web.Mvc;
using Bytes2you.Validation;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Web.Areas.Public.Models.Prices;
using ServiceSystem.Web.Controllers;

namespace ServiceSystem.Web.Areas.Public.Controllers
{
    public class PricesController : BaseController
    {
        private ICategoryService categoriesService;

        public PricesController(ICategoryService service)
        {
            Guard.WhenArgument(service, "service").IsNull().Throw();
            this.categoriesService = service;
        }

        public ActionResult Index()
        {
            var categories =
                this.Cache.Get(
                    "categories",
                    () => this.categoriesService
                        .GetAll(),
                    24 * 60 * 60);

            var prices = this.Mapper.Map<IEnumerable<PricesViewModel>>(categories);

            return this.View(prices);
        }
    }
}
