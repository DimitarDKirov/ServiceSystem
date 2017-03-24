﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Web.ViewModels
{
    public class UnitViewModel : IMapFrom<Unit>, IHaveCustomMappings
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
