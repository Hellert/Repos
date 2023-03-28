using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BusinessSolutions.Models;

namespace BusinessSolutions.Data
{
    public class ProviderRepository : AbstractRepository<Provider>
    {
        public ProviderRepository(BusinessSolutionsContext context) : base(context) { }
    }
}
