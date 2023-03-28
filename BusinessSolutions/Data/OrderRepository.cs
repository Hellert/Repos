using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BusinessSolutions.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessSolutions.Data
{
    public class OrderRepository : AbstractRepository<Order>
    {
        public OrderRepository(BusinessSolutionsContext context) : base(context) { }

    }
}
