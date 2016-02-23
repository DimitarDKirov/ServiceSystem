using ServiceSystem.Common;
using ServiceSystem.Services.Data;
using ServiceSystem.Web.Infrastructure.Mapping;
using ServiceSystem.Web.ViewModels.ListOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceSystem.Web.Controllers
{
    public class ListOrdersController : BaseController
    {
        private IOrderService orderService;

        public ListOrdersController(IOrderService orders)
        {
            this.orderService = orders;
        }

        public ActionResult Pending(int? page)
        {
            int currentPage = page ?? 1;

            var orders = this.orderService
                .ListPaged(currentPage)
                .To<ListedOrderViewModel>()
                .ToList();

            var allItemsCount = this.orderService.CountPending();
            var totalPages = (int)Math.Ceiling(allItemsCount / (decimal)GlobalConstants.PageSize);

            var model = new ListOrdersViewModel
            {
                CurrentPage = currentPage,
                PagesNumber = totalPages,
                Orders = orders
            };

            return this.View(model);

        }
    }
}