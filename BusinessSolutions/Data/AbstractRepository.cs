using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BusinessSolutions.Models;
using BusinessSolutions.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BusinessSolutions.Data
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : class, new()
    {
        public BusinessSolutionsContext _context;
        internal DbSet<T> dbSet;

        protected AbstractRepository(BusinessSolutionsContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public T Get(int id, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            
            return dbSet.Find(id);
        }
        public ICollection<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }
        public virtual IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }
        public void Create (T entity)
        {
            dbSet.Add(entity);
        }
        public void Update (T entity)
        {
            dbSet.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void Delete (int id)
        {
            IQueryable<T> query = dbSet;
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }
    }
}

