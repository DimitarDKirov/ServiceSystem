using AutoMapper;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Web.ViewModels.ListOrders
{
    public class ListedUnitViewModel : IMapFrom<Unit>, IHaveCustomMappings
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Category { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Unit, ListedUnitViewModel>()
                  .ForMember(u => u.Brand, opt => opt.MapFrom(unit => unit.Brand.Name))
                  .ForMember(u => u.Category, opt => opt.MapFrom(unit => unit.Category.Name));
        }
    }
}
