using System;
using AutoMapper;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Services.Data.Models
{
    public class UnitModel : IMapFrom<Unit>, IMapTo<Unit>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public string Brand { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Unit, UnitModel>()
                .ForMember(um => um.Brand, opt => opt.MapFrom(u => u.Brand.Name));
            configuration.CreateMap<Unit, UnitModel>()
                .ForMember(um => um.Category, opt => opt.MapFrom(u => u.Category.Name));

            configuration.CreateMap<UnitModel, Unit>()
                .ForMember(u => u.Brand, opt => opt.Ignore());
            configuration.CreateMap<UnitModel, Unit>()
                .ForMember(u => u.Category, opt => opt.Ignore());
        }
    }
}
