﻿using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Web.ViewModels.CreateOrder
{
    public class CategoriesComboModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
