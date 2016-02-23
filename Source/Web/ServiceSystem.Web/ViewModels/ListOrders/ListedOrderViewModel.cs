namespace ServiceSystem.Web.ViewModels.ListOrders
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;

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