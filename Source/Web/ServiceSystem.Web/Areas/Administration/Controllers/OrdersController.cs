// using System.Linq;
// using System.Web.Mvc;
// using Kendo.Mvc.Extensions;
// using Kendo.Mvc.UI;
// using ServiceSystem.Data.Models;
// using ServiceSystem.Infrastructure;
// using ServiceSystem.Infrastructure.Mapping;
// using ServiceSystem.Services.Data;
// using ServiceSystem.Services.Data.Contracts;
// using ServiceSystem.Services.Data.Models;
// using ServiceSystem.Web.Areas.Administration.Models.Orders;
// using ServiceSystem.Web.Controllers;

// namespace ServiceSystem.Web.Areas.Administration.Controllers
// {
//    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
//    public class OrdersController : BaseController
//    {
//        private IOrderService orderService;

// public OrdersController(IOrderService orders)
//        {
//            this.orderService = orders;
//        }

// public ActionResult Index()
//        {
//            return this.View();
//        }

// public ActionResult Orders_Read([DataSourceRequest]DataSourceRequest request)
//        {
//            // TODO check AsQueryable
//            var orders = this.orderService.GetAll();
//            DataSourceResult result = orders
//                .AsQueryable()
//                .To<OrdersModel>()
//                .ToDataSourceResult(request);

// return this.Json(result);
//        }

// [AcceptVerbs(HttpVerbs.Post)]
//        public ActionResult Orders_Create([DataSourceRequest]DataSourceRequest request, OrdersModel order)
//        {
//            if (this.ModelState.IsValid)
//            {
//                var entity = new OrderModel
//                {
//                    RepairStartDate = order.RepairStartDate,
//                    RepairEndDate = order.RepairEndDate,
//                    DeliverDate = order.DeliverDate,
//                    Status = order.Status,
//                    ProblemDescription = order.ProblemDescription,
//                    Solution = order.Solution,
//                    WarrantyStatus = order.WarrantyStatus,
//                    WarrantyCard = order.WarrantyCard,
//                    WarrantyDate = order.WarrantyDate,
//                    LabourPrice = order.LabourPrice,
//                    CreatedOn = order.CreatedOn
//                };

// this.orderService.Create(entity);
//            }

// return this.Json(new[] { order }.ToDataSourceResult(request, this.ModelState));
//        }

// [AcceptVerbs(HttpVerbs.Post)]
//        public ActionResult Orders_Update([DataSourceRequest]DataSourceRequest request, OrdersModel order)
//        {
//            var orderStored = this.orderService.GetById(order.Id);

// if (this.ModelState.IsValid)
//            {
//                orderStored.RepairStartDate = order.RepairStartDate;
//                orderStored.RepairEndDate = order.RepairEndDate;
//                orderStored.DeliverDate = order.DeliverDate;
//                orderStored.Status = order.Status;
//                orderStored.ProblemDescription = order.ProblemDescription;
//                orderStored.Solution = order.Solution;
//                orderStored.WarrantyStatus = order.WarrantyStatus;
//                orderStored.WarrantyCard = order.WarrantyCard;
//                orderStored.WarrantyDate = order.WarrantyDate;
//                orderStored.LabourPrice = order.LabourPrice;
//                orderStored.CreatedOn = order.CreatedOn;

// this.orderService.Update(orderStored);
//            }

// return this.Json(new[] { order }.ToDataSourceResult(request, this.ModelState));
//        }

// [AcceptVerbs(HttpVerbs.Post)]
//        public ActionResult Orders_Destroy([DataSourceRequest]DataSourceRequest request, OrdersModel order)
//        {
//            if (this.ModelState.IsValid)
//            {
//                this.orderService.Delete(order.Id);
//            }

// return this.Json(new[] { order }.ToDataSourceResult(request, this.ModelState));
//        }
//    }
// }
