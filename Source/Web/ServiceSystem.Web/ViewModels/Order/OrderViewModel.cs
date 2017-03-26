using System;
using System.ComponentModel.DataAnnotations;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Web.ViewModels.Order
{
    public class OrderViewModel : IMapFrom<OrderModel>
    {
        public UnitViewModel Unit { get; set; }

        public CustomerViewCreateModel Customer { get; set; }

        public int Id { get; set; }

        [Display(Name = "Accepted at")]
        public DateTime CreatedOn { get; set; }

        public string OrderPublicId { get; set; }

        [Display(Name = "Warranty")]
        public string WarrantyStatus { get; set; }

        public string WarrantyCard { get; set; }

        [Display(Name = "Problem")]
        public string ProblemDescription { get; set; }
    }
}
