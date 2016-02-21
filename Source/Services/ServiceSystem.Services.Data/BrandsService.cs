namespace ServiceSystem.Services.Data
{
    using ServiceSystem.Data.Common;
    using ServiceSystem.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BrandsService : IBrandsService
    {
        private IDbRepository<Brand> brandsRepo;

        public BrandsService(IDbRepository<Brand> brands)
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
