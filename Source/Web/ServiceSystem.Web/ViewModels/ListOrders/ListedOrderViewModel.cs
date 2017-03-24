using System;
using System.ComponentModel.DataAnnotations;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Web.ViewModels.ListOrders
{
    public class ListedOrderViewModel : IMapFrom<Order>
    {
        public ListedUnitViewModel Unit { get; set; }

        public int Id { get; set; }

        [Display(Name = "Accepted at")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Warranty")]
        public WarrantyStatus WarrantyStatus { get; set; }

        [Display(Name = "Problem")]
        public string ProblemDescription { get; set; }
    }
}
