using ServiceSystem.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceSystem.Web.Controllers
{
    public class ListOrdersController:BaseController
    {
        private IOrderService orderService;

        public ActionResult Pending()
        {

        }
    }
}