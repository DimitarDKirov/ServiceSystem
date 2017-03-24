using System;
using System.ComponentModel.DataAnnotations;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Web.ViewModels.ViewOrder
{
    public class OrderViewModel : IMapFrom<Order>
    {
        public UnitViewModel Unit { get; set; }

        public CustomerViewModel Customer { get; set; }

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
