using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;

namespace ServiceSystem.Services.Data
{
    public class CustomerService : ICustomerService
    {
        private IEfDbRepository<Customer> customerService;

        public CustomerService(IEfDbRepository<Customer> customers)
        {
            this.customerService = customers;
        }

        public Customer Create(string name, string phone, string email)
        {
            var customer = new Customer()
            {
                Name = name,
                Email = email,
                Phone = phone
            };

            this.customerService.Add(customer);
            this.customerService.Save();

            return customer;
        }

        public Customer FindById(int id)
        {
            return this.customerService.GetById(id);
        }
    }
}
