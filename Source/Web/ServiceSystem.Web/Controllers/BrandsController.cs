namespace ServiceSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Services.Data;

    public class BrandsController : BaseController
    {
        private IBrandsService brandsService;

        public BrandsController(IBrandsService brands)
        {
            this.brandsService = brands;
        }

        public JsonResult Find(string brand)
        {
            var brands = this.brandsService.FindByName(brand).ToArray();
            return this.Json(brands, JsonRequestBehavior.AllowGet);
        }
    }
}
