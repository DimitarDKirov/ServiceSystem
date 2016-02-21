namespace ServiceSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ServiceSystem.Services.Data;
    using ServiceSystem.Web.ViewModels.CreateOrder;
    using ViewModels.ViewOrder;
    public class ViewOrderController : BaseController
    {
        private IOrderService orderService;

        public ViewOrderController(IOrderService orders)
        {
            this.orderService = orders;
        }

        public ActionResult AfterCreation(int id)
        {
            var order = this.orderService.GetById(id);
            if (order == null)
            {
                this.TempData["Error"] = "Order can not be found";
                return this.View();
            }

            var orderView = this.Mapper.Map<OrderViewModel>(order);
            return this.View(orderView);
        }
    }
}