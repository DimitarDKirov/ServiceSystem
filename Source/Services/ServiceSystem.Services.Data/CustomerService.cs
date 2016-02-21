namespace ServiceSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ServiceSystem.Data.Common;
    using ServiceSystem.Data.Models;

    public class CustomerService : ICustomerService
    {
        private IDbRepository<Customer> customerService;

        public CustomerService(IDbRepository<Customer> customers)
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
