using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ServiceSystem.Infrastructure;
using ServiceSystem.Infrastructure.PublicCodeProvider;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Web.ViewModels.Order;
using ServiceSystem.Data.Models;
using Microsoft.AspNet.Identity;
using Bytes2you.Validation;

namespace ServiceSystem.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.EngineerRoleName)]
    public class OrderController : BaseController
    {
        private ICategoryService categoryService;
        private IOrderService orderService;

        public OrderController(ICategoryService categoryService, IOrderService ordersService)
        {
            Guard.WhenArgument(categoryService, "categoryService").IsNull().Throw();
            Guard.WhenArgument(ordersService, "ordersService").IsNull().Throw();

            this.categoryService = categoryService;
            this.orderService = ordersService;
        }

        public ActionResult Add()
        {
            var model = new OrderCreateModel
            {
                Categories = this.GetCategories()
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(OrderCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = this.GetCategories();
                return this.View(model);
            }

            OrderModel orderCreated = null;

            try
            {
                var orderModel = this.Mapper.Map<OrderModel>(model);
                orderModel.UserId = this.User.Identity.GetUserId();
                orderCreated = this.orderService.Create(orderModel);
            }
            catch (Exception)
            {
                this.TempData["Error"] = "Order can not be created";
                this.Response.StatusCode = 400;
                model.Categories = this.GetCategories();
                return this.View(model);
            }

            this.TempData["Success"] = "Order created successfully";
            return this.RedirectToAction("Slip", new { id = orderCreated.Id });
        }

        public ActionResult Slip(int id)
        {
            var order = this.orderService.GetById(id);
            if (order == null)
            {
                this.TempData["Error"] = "Order can not be found";
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View("Error");
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
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.View("Error");
            }

            var orderViewModel = this.Mapper.Map<OrderViewModel>(order);
            bool isEditable = orderViewModel.Status != Status.Pending
                && orderViewModel.Status != Status.Delivered
                && order.UserId == this.User.Identity.GetUserId();

            orderViewModel.IsEditable = isEditable;
            return this.View(orderViewModel);
        }

        private IEnumerable<SelectListItem> GetCategories()
        {
            var categories =
                this.Cache.Get(
                    "categories",
                    () => this.categoryService
                        .GetAll(),
                    24 * 60 * 60);

            return categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
        }
    }
}
