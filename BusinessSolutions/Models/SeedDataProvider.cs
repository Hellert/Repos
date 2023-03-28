using BusinessSolutions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

namespace BusinessSolutions.Models

{
    public class SeedDataProvider
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BusinessSolutionsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BusinessSolutionsContext>>()))
            {
                if (context == null || context.Order == null)
                {
                    throw new ArgumentNullException("Null OrderContext");
                }

                if (context.Provider.Any())
                {
                    return;
                }


                var provider1 = new Provider
                {

                    Name = "Provider1"
                };
                var provider2 = new Provider
                {

                    Name = "Provider2"
                };
                var provider3 = new Provider
                {

                    Name = "Provider3"
                };

                var providers = new Provider[]
                    {
                        provider1,
                        provider2,
                        provider3
                    };

                context.AddRange(providers);
                context.SaveChanges();

            }
        }
    }
}

