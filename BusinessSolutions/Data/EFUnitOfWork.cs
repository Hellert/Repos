using System;
using BusinessSolutions.Models;
using BusinessSolutions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessSolutions.Data
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private BusinessSolutionsContext _context;
        private OrderRepository _orderRepository;
        private OrderItemRepository _orderItemRepository;
        private ProviderRepository _providerRepository;

        public EFUnitOfWork(DbContextOptions<BusinessSolutionsContext> options)
        {
            _context = new BusinessSolutionsContext(options);
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_context);
                return _orderRepository;
            }
        }
        public IRepository<OrderItem> OrderItems
        {
            get
            {
                if (_orderItemRepository == null)
                    _orderItemRepository = new OrderItemRepository(_context);
                return _orderItemRepository;
            }
        }

        public IRepository<Provider> Provider
        {
            get
            {
                if (_providerRepository == null)
                    _providerRepository = new ProviderRepository(_context);
                return _providerRepository;
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
