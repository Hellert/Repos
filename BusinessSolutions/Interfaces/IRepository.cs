using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BusinessSolutions.Interfaces
{
    public interface IRepository<T> where T : class
    {
        ICollection<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        T Get(int id, string? includeProperties = null);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
