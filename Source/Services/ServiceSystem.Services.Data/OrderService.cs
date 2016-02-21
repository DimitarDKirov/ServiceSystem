namespace ServiceSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ServiceSystem.Data.Models;
    using ServiceSystem.Data.Common;

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

        public Order Create(Order order)
        {
            this.ordersRepository.Add(order);
            this.ordersRepository.Save();
            return order;
        }

        public Order GetById(int id)
        {
            return this.ordersRepository.GetById(id);
        }
    }
}
