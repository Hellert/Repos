using BusinessSolutions.Models;

namespace BusinessSolutions.ViewModels
{
    public class OrderVM
    {
        public Order Order { get; set; }
        public Provider Provider { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
