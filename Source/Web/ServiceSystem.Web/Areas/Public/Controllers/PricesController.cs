﻿namespace ServiceSystem.Web.Areas.Public.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Models.Prices;
    using Services.Data;
    using Web.Controllers;
    using Services.Data.Contracts;
    using ServiceSystem.Infrastructure.Mapping;

    public class PricesController : BaseController
    {
        private ICategoryService categoriesService;

        public PricesController(ICategoryService service)
        {
            this.categoriesService = service;
        }

        public ActionResult Index()
        {
            // TODO remove asQuerabley
            var prices = this.Cache.Get(
                "PricesPublic",
                () => this.categoriesService
                .GetAll()
                .AsQueryable()
                .To<PricesViewModel>()
                .ToList(),
                24 * 60 * 60);

            return this.View(prices);
        }
    }
}
