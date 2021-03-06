﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Web.Areas.Public.Models.OrderStatus
{
    public class UnitViewModel : IMapFrom<UnitModel>, IHaveCustomMappings
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }

        public string Category { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Unit, UnitViewModel>()
                 .ForMember(u => u.Brand, opt => opt.MapFrom(unit => unit.Brand.Name))
                 .ForMember(u => u.Category, opt => opt.MapFrom(unit => unit.Category.Name));
        }
    }
}
