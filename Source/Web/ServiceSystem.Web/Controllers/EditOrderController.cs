using ServiceSystem.Services.Data;
using ServiceSystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ServiceSystem.Web.Controllers
{
    public class EditOrderController:BaseController
    {
        private IOrderService orderService;

        public EditOrderController(IOrderService orders)
        {
            this.orderService = orders;
        }

        public ActionResult Edit(int id)
        {
            var order = this.orderService.GetById(id);
            if (order == null)
            {
                this.TempData["Error"] = "Order can not be found";
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.View();
            }

            var orderViewModel = this.Mapper.Map<OrderDetailsViewModel>(order);
            return this.View(orderViewModel);
        } 
    }
}