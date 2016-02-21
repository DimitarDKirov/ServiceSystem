namespace ServiceSystem.Services.Data
{
    using ServiceSystem.Data.Models;
    using System.Linq;

    public interface IBrandsService
    {
        IQueryable<string> FindByName(string brand);

        Brand Create(string brand);
    }
}
