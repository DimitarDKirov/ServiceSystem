namespace ServiceSystem.Services.Data
{
    using System.Linq;
    using Common;
    using ServiceSystem.Data.Common;
    using ServiceSystem.Data.Models;

    public class OrderService : IOrderService
    {
        private IDbRepository<Order> ordersRepository;

        public OrderService(IDbRepository<Order> ordersRepo)
        {
            this.ordersRepository = ordersRepo;
        }

        public Order AddPublicId(int orderId, string code)
        {
            var order = this.ordersRepository.GetById(orderId);
            if (order != null)
            {
                order.OrderPublicId = code;
                this.ordersRepository.Save();
            }

            return order;
        }

        public int CountPending()
        {
            var count = this.ordersRepository
                .All()
                .Count(o => o.Status == Status.Pending);

            return count;
        }

        public Order Create(Order order)
        {
            this.ordersRepository.Add(order);
            this.ordersRepository.Save();
            return order;
        }

        public IQueryable<Order> GetAll()
        {
            return this.ordersRepository.All();
        }

        public Order GetById(int id)
        {
            return this.ordersRepository.GetById(id);
        }

        public IQueryable<Order> ListPaged(int page)
        {
            int pageSize = GlobalConstants.PageSize;
            var itemsToSkip = (page - 1) * pageSize;

            var orders = this.ordersRepository
                .All()
                .Where(o => o.Status == Status.Pending)
                .OrderBy(o => o.Id)
                .Skip(itemsToSkip)
                .Take(pageSize);

            return orders;
        }

        public Order Update(Order order)
        {
            this.ordersRepository.Save();
            return order;
        }
    }
}
