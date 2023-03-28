using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BusinessSolutions.Models;
using BusinessSolutions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessSolutions.Data
{
    public class OrderItemRepository : AbstractRepository<OrderItem>
    {
        public OrderItemRepository(BusinessSolutionsContext context) : base(context)  { }
        public override IEnumerable<OrderItem> Find(Func<OrderItem, bool> predicate)
        {
            return _context.OrderItem
                .Include(o => o.Order)
                .Where(predicate).ToList();
        }
    }
}
