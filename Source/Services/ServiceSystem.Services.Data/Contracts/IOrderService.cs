﻿using System.Collections.Generic;
using System.Linq;
using ServiceSystem.Data.Models;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Services.Data.Contracts
{
    public interface IOrderService
    {
        OrderModel Create(OrderModel order);

        OrderModel GetById(int id);

        IEnumerable<OrderModel> ListPaged(int page);

        int Count(Status status);

        OrderModel Update(OrderModel order);

        IEnumerable<OrderModel> GetAll();

        IQueryable<OrderModel> GetAsQuaryable();

        void Delete(int id);

        void Assign(int id, string userId);
    }
}
