namespace ServiceSystem.Web.Controllers
{
    using Services.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

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