using Bytes2you.Validation;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Services.Data
{
    public class UnitService : IUnitService
    {
        // private IEfDbRepository<Unit> unitsRepo;
        private IBrandsService brandService;
        private IMappingService mappringService;
        // private IEfDbRepositorySaveChanges efRepoSaveData;

        public UnitService(/*IEfDbRepository<Unit> unitsRepo,*/ IBrandsService brandService, /*IEfDbRepositorySaveChanges efRepoSaveData,*/ IMappingService mappringService)
        {
            // Guard.WhenArgument(unitsRepo, "unitsRepo").IsNull().Throw();
            Guard.WhenArgument(brandService, "brandService").IsNull().Throw();
            // Guard.WhenArgument(efRepoSaveData, "efRepoSaveData").IsNull().Throw();
            Guard.WhenArgument(mappringService, "mappringService").IsNull().Throw();

            // this.unitsRepo = unitsRepo;
            this.brandService = brandService;
            this.mappringService = mappringService;
            // this.efRepoSaveData = efRepoSaveData;
        }

        // public UnitModel Create(string brandName, string model, string serialNumber, int categoryId)
        // {
        //    var brand = this.brandService.CreateDbModel(brandName);

        // var unit = this.mappringService.Map<Unit>(model);
        //    unit.Brand = brand;

        // this.unitsRepo.Add(unit);
        //    this.efRepoSaveData.SaveChanges();

        // return this.mappringService.Map<UnitModel>(unit);
        // }

        public Unit CreateDbModel(UnitModel model, string brandName)
        {
            var brand = this.brandService.CreateDbModel(brandName);

            var unit = this.mappringService.Map<Unit>(model);
            unit.Brand = brand;

            return unit;
        }
    }
}
