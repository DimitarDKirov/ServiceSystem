using ServiceSystem.Data.Models;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Services.Data.Contracts
{
    public interface IUnitService
    {
        //UnitModel Create(string brand, string model, string serialNumber, int categoryId);

        Unit CreateDbModel(UnitModel model, string brandName);
    }
}
