namespace ServiceSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mapping;
    using Services.Data;
    using ViewModels.ListOrders;
    using ServiceSystem.Infrastructure;
    using ServiceSystem.Infrastructure.Mapping;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.EngineerRoleName)]
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
