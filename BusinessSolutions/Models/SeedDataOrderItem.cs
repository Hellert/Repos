using BusinessSolutions.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessSolutions.Models
{
    public class SeedDataOrderItem
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BusinessSolutionsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BusinessSolutionsContext>>()))
            {
                if (context == null || context.OrderItem == null)
                {
                    throw new ArgumentNullException("Null OrderContext");
                }

                if (context.OrderItem.Any())
                {
                    return;
                }

                var orderItem1 = new OrderItem
                {
                    OrderId = 1,
                    Name = "Order1Item1",
                    Quantity = 10,
                    Unit = "order1unit1"
                };
                var orderItem2 = new OrderItem
                {
                    OrderId = 1,
                    Name = "Order1Item2",
                    Quantity = 5,
                    Unit = "order1unit2"
                };
                var orderItem3 = new OrderItem
                {
                    OrderId = 2,
                    Name = "Order2Item1",
                    Quantity = 2,
                    Unit = "order2unit1"
                };
                var orderItem4 = new OrderItem
                {
                    OrderId = 3,
                    Name = "Order3Item1",
                    Quantity = 3,
                    Unit = "order3unit1"
                };
                var orderItem5 = new OrderItem
                {
                    OrderId = 4,
                    Name = "Order4Item1",
                    Quantity = 4,
                    Unit = "order4unit1"
                };
                var orderItem6 = new OrderItem
                {
                    OrderId = 5,
                    Name = "Order5Item1",
                    Quantity = 5,
                    Unit = "order5unit1"
                };

                var orderitems = new OrderItem[]
                    {
                        orderItem1,
                        orderItem2,
                        orderItem3,
                        orderItem4,
                        orderItem5,
                        orderItem6
                    };

                context.AddRange(orderitems);


                context.SaveChanges();
            }
        }
    }
}
