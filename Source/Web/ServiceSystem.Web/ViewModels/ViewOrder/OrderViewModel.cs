namespace ServiceSystem.Web.ViewModels.ViewOrder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using ServiceSystem.Data.Models;
    using ServiceSystem.Web.Infrastructure.Mapping;

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
