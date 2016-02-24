namespace ServiceSystem.Services.Data
{
    using ServiceSystem.Data.Models;

    public interface IUnitService
    {
        Unit Create(string brand, string model, string serialNumber, int categoryId);
    }
}
