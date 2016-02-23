using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceSystem.Web.ViewModels.ListOrders
{
    public class ListOrdersViewModel
    {
        public IEnumerable<ListedOrderViewModel> Orders { get; set; }

        public int CurrentPage { get; set; }

        public int PagesNumber { get; set; }
    }
}