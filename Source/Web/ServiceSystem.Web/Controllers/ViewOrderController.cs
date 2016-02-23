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
    using System.Net;
    using ViewModels;
    using Data.Models;
    using Microsoft.AspNet.Identity;

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
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View();
            }

            var orderViewModel = this.Mapper.Map<OrderViewModel>(order);
            return this.View(orderViewModel);
        }

        public ActionResult Details(int id)
        {
            var order = this.orderService.GetById(id);
            if (order == null)
            {
                this.TempData["Error"] = "Order can not be found";
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.View();
            }

            var orderViewModel = this.Mapper.Map<OrderDetailsViewModel>(order);
            orderViewModel.IsDeliverable = false;
            if (order.Status == Status.Pending)
            {
                orderViewModel.IsAssignable = true;
                orderViewModel.IsEditable = false;
            }
            else if (order.UserId == this.User.Identity.GetUserId())
            {
                orderViewModel.IsAssignable = false;
                orderViewModel.IsEditable = true;
            }
            else
            {
                orderViewModel.IsAssignable = false;
                orderViewModel.IsEditable = false;
            }

            if (order.Status == Status.Ready)
            {
                orderViewModel.IsDeliverable = true;
            }

            return this.View(orderViewModel);
        }

        [ChildActionOnly]
        public ActionResult CommonDetails(int id)
        {
            var order = this.orderService.GetById(id);
            var orderViewModel = this.Mapper.Map<CommonDetailsViewModel>(order);

            return this.PartialView("_OrderViewPartial", orderViewModel);
        }
    }
}