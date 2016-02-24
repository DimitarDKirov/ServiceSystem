namespace ServiceSystem.Services.Data
{
    using System.Linq;
    using ServiceSystem.Data.Models;

    public interface IOrderService
    {
        Order Create(Order order);

        Order AddPublicId(int orderId, string code);

        Order GetById(int id);

        IQueryable<Order> ListPaged(int page);

        int CountPending();

        Order Update(Order order);

        IQueryable<Order> GetAll();

        void Delete(int id);
    }
}
