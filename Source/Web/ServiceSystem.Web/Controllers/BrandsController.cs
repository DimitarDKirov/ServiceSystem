using System.Linq;
using System.Web.Mvc;
using Bytes2you.Validation;
using ServiceSystem.Services.Data.Contracts;

namespace ServiceSystem.Web.Controllers
{
    public class BrandsController : BaseController
    {
        private IBrandsService brandsService;

        public BrandsController(IBrandsService brandsService)
        {
            Guard.WhenArgument(brandsService, "brandsService").IsNull().Throw();
            this.brandsService = brandsService;
        }

        public JsonResult Find(string brand)
        {
            var brands = this.brandsService.FindByName(brand).ToArray();
            return this.Json(brands, JsonRequestBehavior.AllowGet);
        }
    }
}
