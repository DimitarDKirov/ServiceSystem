namespace ServiceSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ServiceSystem.Data.Common;
    using ServiceSystem.Data.Models;

    public class UnitService : IUnitService
    {
        private IDbRepository<Unit> unitsRepository;
        private IBrandsService brandService;

        public UnitService(IDbRepository<Unit> unitsRepo, IBrandsService brandService)
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
