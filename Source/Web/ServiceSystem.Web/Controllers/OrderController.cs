using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure;
using ServiceSystem.Infrastructure.PublicCodeProvider;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Web.ViewModels.Order;

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
                return this.OrderNotFound();
            }

            var orderViewModel = this.Mapper.Map<OrderViewModel>(order);
            return this.View(orderViewModel);
        }

        public ActionResult Details(int id)
        {
            var order = this.orderService.GetById(id);
            if (order == null)
            {
                return this.OrderNotFound();
            }

            var orderViewModel = this.Mapper.Map<OrderViewModel>(order);
            bool isEditable = this.IsEditable(order);

            orderViewModel.IsEditable = isEditable;
            return this.View(orderViewModel);
        }

        public ActionResult Edit(int id)
        {
            var order = this.orderService.GetById(id);
            if (order == null)
            {
                return this.OrderNotFound();
            }

            if (!this.IsEditable(order))
            {
                return this.NotEditable(id);
            }

            var orderViewModel = this.Mapper.Map<OrderViewModel>(order);
            return this.View(orderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderUpdateModel model)
        {
            var order = this.orderService.GetById(model.Id);
            if (order == null)
            {
                return this.OrderNotFound();
            }

            if (!this.IsEditable(order))
            {
                return this.NotEditable(model.Id);
            }

            if (!this.ModelState.IsValid)
            {
                this.TempData["Error"] = "Input data errors. Look bellow";
                return this.View(model);
            }

            var orderModel = this.Mapper.Map<OrderModel>(model);
            this.orderService.Update(orderModel);

            this.TempData["Success"] = "Order updated";
            return this.RedirectToAction("Details", new { id = order.Id });
        }

        public ActionResult Assign(int id)
        {
            try
            {
                this.orderService.Assign(id, this.User.Identity.GetUserId());
            }
            catch (ArgumentOutOfRangeException e)
            {
                return this.OrderNotFound();
            }
            catch (ArgumentException ex)
            {
                this.TempData["Error"] = ex.Message;
                return this.RedirectToAction("Details", new { id });
            }

            this.TempData["Success"] = "You are assigned to order " + id;
            return this.RedirectToAction("Details", new { id });
        }

        private ActionResult NotEditable(int id)
        {
            this.TempData["Error"] = "You are not allowed to edit this order";
            this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return this.RedirectToAction("Details", new { id = id });
        }

        private bool IsEditable(OrderModel order)
        {
            return order.Status != Status.Pending && order.Status != Status.Delivered && order.UserId == this.User.Identity.GetUserId();
        }

        private ActionResult OrderNotFound()
        {
            this.TempData["Error"] = "Order can not be found";
            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return this.View("Error");
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
