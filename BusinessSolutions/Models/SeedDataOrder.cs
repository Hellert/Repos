using BusinessSolutions.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessSolutions.Models
{
    public class SeedDataOrder
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

                if (context.Order.Any())
                {
                    return;
                }


                var ordernumber1 = new Order
                {
                    Number = "OrderNumber1",
                    Date = DateTime.Parse("2023-03-01"),
                    ProviderId = 1
                };
                var ordernumber2 = new Order
                {
                    Number = "OrderNumber2",
                    Date = DateTime.Parse("2023-03-02"),
                    ProviderId = 1
                };
                var ordernumber3 = new Order
                {
                    Number = "OrderNumber1",
                    Date = DateTime.Parse("2023-03-03"),
                    ProviderId = 2
                };
                var ordernumber4 = new Order
                {
                    Number = "OrderNumber2",
                    Date = DateTime.Parse("2023-03-04"),
                    ProviderId = 2
                };
                var ordernumber5 = new Order
                {
                    Number = "OrderNumber1",
                    Date = DateTime.Parse("2023-03-05"),
                    ProviderId = 3
                };

                var orders = new Order[]
                    {
                        ordernumber1,
                        ordernumber2,
                        ordernumber3,
                        ordernumber4,
                        ordernumber5
                    };

                context.AddRange(orders);
                context.SaveChanges();
            }
        }
    }
}
