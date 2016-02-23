namespace ServiceSystem.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class OrderDetailsViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public UnitViewModel Unit { get; set; }

        public CustomerViewModel Customer { get; set; }

        [Display(Name = "Order number")]
        public int Id { get; set; }

        [Display(Name = "Accepted at")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Warranty")]
        public WarrantyStatus WarrantyStatus { get; set; }

        [Display(Name = "Problem")]
        [DataType(DataType.MultilineText)]
        public string ProblemDescription { get; set; }

        public Status Status { get; set; }

        public string Solution { get; set; }

        [Display(Name = "Price")]
        public decimal LabourPrice { get; set; }

        [Display(Name = "Repair start date")]
        public DateTime? RepairStartDate { get; set; }

        [Display(Name = "Repair finish date")]
        public DateTime? RepairEndDate { get; set; }

        [Display(Name = "Delivery date")]
        public DateTime? DeliverDate { get; set; }

        [Display(Name = "Engineer")]
        public string User { get; set; }

        public bool IsEditable { get; set; }

        public bool IsDeliverable { get; set; }

        public bool IsAssignable { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Order, OrderDetailsViewModel>()
                .ForMember(o => o.User, opt => opt.MapFrom(ord => ord.User == null ? null : ord.User.UserName));
        }
    }
}
