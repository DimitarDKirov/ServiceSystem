using ServiceSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSystem.Services.Data
{
    public interface ICategoriesService
    {
        IQueryable<Category> GetAllForCombo();
    }
}
