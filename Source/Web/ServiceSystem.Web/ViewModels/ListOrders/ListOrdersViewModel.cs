namespace ServiceSystem.Web.ViewModels.ListOrders
{
    using System.Collections.Generic;

    public class ListOrdersViewModel
    {
        public IEnumerable<ListedOrderViewModel> Orders { get; set; }

        public int CurrentPage { get; set; }

        public int PagesNumber { get; set; }
    }
}
