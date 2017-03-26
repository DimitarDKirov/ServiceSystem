using Bytes2you.Validation;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Services.Data
{
    public class CustomerService : ICustomerService
    {
        private IEfDbRepository<Customer> customersRepo;
        //private IEfDbRepositorySaveChanges efRepoSaveChanges;
        private IMappingService mappingService;

        public CustomerService(IEfDbRepository<Customer> customersRepo,/* IEfDbRepositorySaveChanges efRepoSaveChanges,*/ IMappingService mappingService)
        {
            Guard.WhenArgument(customersRepo, "customersRepo").IsNull().Throw();
            //Guard.WhenArgument(efRepoSaveChanges, "efRepoSaveChanges").IsNull().Throw();
            Guard.WhenArgument(mappingService, "mappingService").IsNull().Throw();

            this.customersRepo = customersRepo;
            //this.efRepoSaveChanges = efRepoSaveChanges;
            this.mappingService = mappingService;
        }

        // TODO check if needed
        //public CustomerModel Create(CustomerModel model)
        //{
        //    var customer = this.mappingService.Map<Customer>(model);

        //    this.customersRepo.Add(customer);
        //    this.efRepoSaveChanges.SaveChanges();

        //    return this.mappingService.Map<CustomerModel>(customer);
        //}

        public Customer CreateDbModel(CustomerModel model)
        {
            var customer = this.mappingService.Map<Customer>(model);

            return customer;
        }

        public CustomerModel FindById(int id)
        {
            var customer = this.customersRepo.GetById(id);
            return this.mappingService.Map<CustomerModel>(customer);
        }
    }
}
