using System.Linq;
using ServiceSystem.Data.Common;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;

namespace ServiceSystem.Services.Data
{
    public class BrandsService : IBrandsService
    {
        private IEfDbRepository<Brand> brandsRepo;

        public BrandsService(IEfDbRepository<Brand> brands)
        {
            this.brandsRepo = brands;
        }

        public Brand Create(string name)
        {
            var brand = this.brandsRepo
                .All()
                .Where(b => b.Name == name)
                .FirstOrDefault();

            if (brand == null)
            {
                brand = new Brand
                {
                    Name = name
                };

                this.brandsRepo.Add(brand);
                this.brandsRepo.Save();
            }

            return brand;
        }

        public IQueryable<string> FindByName(string brand)
        {
            return this.brandsRepo
                .All()
                .Where(b => b.Name.ToUpper().Contains(brand.ToUpper()))
                .Select(b => b.Name);
        }
    }
}
