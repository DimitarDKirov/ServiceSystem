namespace ServiceSystem.Services.Data
{
    using ServiceSystem.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICustomerService
    {
        Customer Create(string name, string phone, string email);

        Customer FindById(int id);
    }
}
