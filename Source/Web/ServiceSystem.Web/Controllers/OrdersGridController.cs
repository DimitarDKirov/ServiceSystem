﻿namespace ServiceSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data;
    using ViewModels.OrdersGrid;

    public class OrdersGridController : BaseController
    {
        private IOrderService orderService;

        public OrdersGridController(IOrderService orders)
        {
            this.orderService = orders;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Orders_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Order> orders = this.orderService.GetAll();
            DataSourceResult result = orders
                .To<OrdersGridViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }
    }
}