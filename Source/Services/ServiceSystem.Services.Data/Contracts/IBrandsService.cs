using System.Collections.Generic;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Services.Data.Contracts
{
    public interface IBrandsService
    {
        IEnumerable<string> FindByName(string brand);

        BrandModel Create(string brand);
    }
}
