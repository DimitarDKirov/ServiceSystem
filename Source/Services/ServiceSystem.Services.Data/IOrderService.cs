using ServiceSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSystem.Services.Data
{
   public interface IOrderService
    {
        Order Create(Order order);

        Order AddPublicId(int orderId, string code);

        Order GetById(int id);

        IQueryable<Order> ListPaged(int page);

        int CountPending();

        Order Update(Order order);

        IQueryable<Order> GetAll();
    }
}
