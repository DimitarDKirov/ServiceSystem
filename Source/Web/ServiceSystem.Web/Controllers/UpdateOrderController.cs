namespace ServiceSystem.Web.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;

    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data;
    using ViewModels.UpdateOrder;
    using ServiceSystem.Infrastructure;
    using Services.Data.Contracts;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.EngineerRoleName)]
    public class UpdateOrderController : BaseController
    {
        private IOrderService orderService;

        public UpdateOrderController(IOrderService orders)
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

            if (order.Status == Status.Pending || order.Status == Status.Delivered || order.UserId != this.User.Identity.GetUserId())
            {
                this.TempData["Error"] = "You are not allowed to edit this order";
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return this.RedirectToAction("Details", "ViewOrder", new { id = id });
            }

            var orderViewModel = this.Mapper.Map<UpdateOrderModel>(order);
            return this.View(orderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateOrderModel model)
        {
            var order = this.orderService.GetById(model.Id);
            if (order == null)
            {
                this.TempData["Error"] = "Order can not be found";
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.View("Error");
            }

            if (order.UserId != this.User.Identity.GetUserId())
            {
                this.TempData["Error"] = "Order is not assigned to you";
                this.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return this.RedirectToAction("Details", "ViewOrder", new { id = model.Id });
            }

            if (order.Status == Status.Delivered)
            {
                this.TempData["Error"] = "Delivered orders can not be updated";
                this.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return this.RedirectToAction("Details", "ViewOrder", new { id = model.Id });
            }

            if (model.Status == Status.Delivered)
            {
                this.ModelState.AddModelError("Status", "Order can be set to status Delivered only through View Details menu");
            }

            if (model.Status == Status.Ready && string.IsNullOrEmpty(model.Solution))
            {
                this.ModelState.AddModelError("Solution", "Solution is required for orders with status Ready");
            }

            if (!this.ModelState.IsValid)
            {
                this.TempData["Error"] = "Input data errors. Look bellow";
                return this.View(model);
            }

            order.ProblemDescription = model.ProblemDescription;
            order.Solution = model.Solution;
            order.Status = model.Status;
            order.WarrantyStatus = model.WarrantyStatus;
            order.LabourPrice = model.LabourPrice;

            if (model.Status == Status.Ready)
            {
                order.RepairEndDate = DateTime.Now;
            }

            this.orderService.Update(order);
            this.TempData["Success"] = "Order updated";
            return this.RedirectToAction("Details", "ViewOrder", new { id = order.Id });
        }

        public ActionResult Assign(int id)
        {
            var order = this.orderService.GetById(id);
            if (order == null)
            {
                this.TempData["Error"] = "Order can not be found";
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.View("Error");
            }

            if (order.Status == Status.Delivered)
            {
                this.TempData["Error"] = "Delivered orders can not be updated";
                this.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return this.RedirectToAction("Details", "ViewOrder", new { id = id });
            }

            order.Status = Status.InProcess;
            order.RepairStartDate = DateTime.Now;
            order.UserId = this.User.Identity.GetUserId();

            this.orderService.Update(order);
            this.TempData["Success"] = "Order assigned";
            return this.RedirectToAction("Edit", new { id = order.Id });
        }

        public ActionResult Deliver(int id)
        {
            var order = this.orderService.GetById(id);
            if (order == null)
            {
                this.TempData["Error"] = "Order can not be found";
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.View("Error");
            }

            order.Status = Status.Delivered;
            order.DeliverDate = DateTime.Now;

            this.orderService.Update(order);
            this.TempData["Success"] = "Order delivered";
            return this.RedirectToAction("Details", "ViewOrder", new { id = order.Id });
        }
    }
}
