using System.Collections.Generic;
using ServiceSystem.Data.Models;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Services.Data.Contracts
{
    public interface IBrandsService
    {
        IEnumerable<string> FindByName(string brand);

        Brand CreateDbModel(string name);
    }
}
