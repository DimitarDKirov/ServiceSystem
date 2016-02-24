using ServiceSystem.Web.Areas.Public.Models.OrderStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceSystem.Web.Areas.Public.Controllers
{
    public class OrderStatusController : Controller
    {
        // GET: Public/OrderStatus
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(OrderSearchViewModel input)
        {

            return null;
        }
    }
}