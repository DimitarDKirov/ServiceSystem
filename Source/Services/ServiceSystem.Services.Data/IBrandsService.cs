namespace ServiceSystem.Services.Data
{
    using System.Linq;
    using ServiceSystem.Data.Models;

    public interface IBrandsService
    {
        IQueryable<string> FindByName(string brand);

        Brand Create(string brand);
    }
}
