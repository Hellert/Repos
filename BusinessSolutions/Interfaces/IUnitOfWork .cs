using System;
using BusinessSolutions.Models;

namespace BusinessSolutions.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Order> Orders { get; }
        IRepository<OrderItem> OrderItems { get; }
        IRepository<Provider> Provider { get; }
        void Commit();
    }
}
