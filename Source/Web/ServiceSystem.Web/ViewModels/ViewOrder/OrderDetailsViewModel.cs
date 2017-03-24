using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Web.ViewModels.ViewOrder
{
    public class OrderDetailsViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        [Display(Name = "Order number")]
        public int Id { get; set; }

        [Display(Name = "Problem")]
        [DataType(DataType.MultilineText)]
        public string ProblemDescription { get; set; }

        [DataType(DataType.MultilineText)]
        public string Solution { get; set; }

        [Display(Name = "Price")]
        public decimal LabourPrice { get; set; }

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
