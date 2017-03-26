﻿using System.Collections.Generic;
using System.Linq;
using Bytes2you.Validation;
using ServiceSystem.Data.Common.Contracts;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure;
using ServiceSystem.Infrastructure.Mapping;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;
using ServiceSystem.Services.Data.Contracts;
using ServiceSystem.Infrastructure.PublicCodeProvider;

namespace ServiceSystem.Services.Data
{
    public class OrderService : IOrderService
    {
        private ICustomerService customerService;
        private IEfDbRepositorySaveChanges efRepoSaveData;
        private IMappingService mappingService;
        private IEfDbRepository<Order> ordersRepo;
        private IPublicCodeProvider publicCodeProvider;
        private IUnitService unitService;

        public OrderService(IEfDbRepository<Order> ordersRepo, IEfDbRepositorySaveChanges efRepoSaveData, IMappingService mappringService, IUnitService unitService, ICustomerService customerService, IPublicCodeProvider publicCodeProvider)
        {
            Guard.WhenArgument(ordersRepo, "ordersRepo").IsNull().Throw();
            Guard.WhenArgument(efRepoSaveData, "efRepoSaveData").IsNull().Throw();
            Guard.WhenArgument(mappringService, "mappringService").IsNull().Throw();
            Guard.WhenArgument(unitService, "unitService").IsNull().Throw();
            Guard.WhenArgument(customerService, "customerService").IsNull().Throw();
            Guard.WhenArgument(publicCodeProvider, "publicCodeProvider").IsNull().Throw();

            this.ordersRepo = ordersRepo;
            this.efRepoSaveData = efRepoSaveData;
            this.mappingService = mappringService;
            this.unitService = unitService;
            this.customerService = customerService;
            this.publicCodeProvider = publicCodeProvider;
        }

        public int Count(Status status)
        {
            var count = this.ordersRepo
                .All()
                .Count(o => o.Status == status);

            return count;
        }

        public OrderModel Create(OrderModel orderModel)
        {
            var customerModel = orderModel.Customer;
            var customer = this.customerService.CreateDbModel(customerModel);

            var unitModel = orderModel.Unit;
            var unit = this.unitService.CreateDbModel(unitModel, unitModel.Brand);

            var order = this.mappingService.Map<Order>(orderModel);
            order.Unit = unit;
            order.Customer = customer;
            var storedOrder = this.ordersRepo.Add(order);
            this.efRepoSaveData.SaveChanges();

            var publicCode = this.publicCodeProvider.Encode(storedOrder.Id, storedOrder.Customer.Name);
            storedOrder.OrderPublicId = publicCode;
            this.efRepoSaveData.SaveChanges();

            orderModel.Id = storedOrder.Id;
            return orderModel;
        }

        public void Delete(int id)
        {
            var order = this.ordersRepo.GetById(id);
            this.ordersRepo.Delete(order);
            this.efRepoSaveData.SaveChanges();
        }

        public IEnumerable<OrderModel> GetAll()
        {
            return this.ordersRepo
                .All()
                .To<OrderModel>()
                .ToList();
        }

        public OrderModel GetById(int id)
        {
            var order = this.ordersRepo.GetById(id);
            return this.mappingService.Map<OrderModel>(order);
        }

        public IEnumerable<OrderModel> ListPaged(int page)
        {
            int pageSize = GlobalConstants.PageSize;
            var itemsToSkip = (page - 1) * pageSize;

            var orders = this.ordersRepo
                .All()
                .Where(o => o.Status == Status.Pending)
                .OrderBy(o => o.Id)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .To<OrderModel>()
                .ToList();

            return orders;
        }

        public OrderModel Update(OrderModel orderModel)
        {
            var order = this.mappingService.Map<Order>(orderModel);
            this.ordersRepo.Update(order);
            this.efRepoSaveData.SaveChanges();
            return orderModel;
        }
    }
}
