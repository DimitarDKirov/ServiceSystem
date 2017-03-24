namespace ServiceSystem.Services.Data
{
    using ServiceSystem.Data.Models;

    public interface ICustomerService
    {
        Customer Create(string name, string phone, string email);

        Customer FindById(int id);
    }
}
