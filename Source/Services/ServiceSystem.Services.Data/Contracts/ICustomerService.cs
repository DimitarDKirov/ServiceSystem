using ServiceSystem.Data.Models;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Services.Data.Contracts
{
    public interface ICustomerService
    {
       // CustomerModel Create(CustomerModel model);

        Customer CreateDbModel(CustomerModel model);

        CustomerModel FindById(int id);
    }
}
