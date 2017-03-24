using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Services.Data.Contracts;

namespace ServiceSystem.Services.Data
{
    public class UnitService : IUnitService
    {
        private IEfDbRepository<Unit> unitsRepository;
        private IBrandsService brandService;

        public UnitService(IEfDbRepository<Unit> unitsRepo, IBrandsService brandService)
        {
            this.unitsRepository = unitsRepo;
            this.brandService = brandService;
        }

        public Unit Create(string brandName, string model, string serialNumber, int categoryId)
        {
            var brand = this.brandService.Create(brandName);

            var unit = new Unit()
            {
                BrandId = brand.Id,
                Model = model,
                SerialNumber = serialNumber,
                CategoryId = categoryId
            };

            this.unitsRepository.Add(unit);
            this.unitsRepository.Save();

            return unit;
        }
    }
}
