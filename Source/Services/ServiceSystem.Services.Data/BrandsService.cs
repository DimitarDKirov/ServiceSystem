using System.Collections.Generic;
using System.Linq;
using Bytes2you.Validation;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Services.Data
{
    public class BrandsService : IBrandsService
    {
        private IEfDbRepository<Brand> brandsRepo;
        //private IEfDbRepositorySaveChanges efRepoSaveData;
        //private IMappingService mappringService;

        public BrandsService(IEfDbRepository<Brand> brandsRepo/*, IEfDbRepositorySaveChanges efRepoSaveData, IMappingService mappringService*/)
        {
            Guard.WhenArgument(brandsRepo, "brandsRepo").IsNull().Throw();
            //Guard.WhenArgument(efRepoSaveData, "efRepoSaveData").IsNull().Throw();
            //Guard.WhenArgument(mappringService, "mappringService").IsNull().Throw();

            this.brandsRepo = brandsRepo;
            //this.efRepoSaveData = efRepoSaveData;
            //this.mappringService = mappringService;
        }

        //public BrandModel Create(string name)
        //{
        //    Brand brand = this.FindExactByName(name);

        //    if (brand == null)
        //    {
        //        brand = new Brand
        //        {
        //            Name = name
        //        };

        //        this.brandsRepo.Add(brand);
        //        this.efRepoSaveData.SaveChanges();
        //    }

        //    return this.mappringService.Map<BrandModel>(brand);
        //}

        public Brand CreateDbModel(string name)
        {
            var brand = this.FindExactByName(name);

            if (brand == null)
            {
                brand = new Brand
                {
                    Name = name
                };
            }

            return brand;
        }

        public IEnumerable<string> FindByName(string brand)
        {
            if (brand == null)
            {
                return null;
            }

            return this.brandsRepo
                .All()
                .Where(b => b.Name.ToUpper().Contains(brand.ToUpper()))
                .Select(b => b.Name)
                .ToList();
        }

        private Brand FindExactByName(string name)
        {
            return this.brandsRepo
                            .All()
                            .Where(b => b.Name == name)
                            .FirstOrDefault();
        }
    }
}
