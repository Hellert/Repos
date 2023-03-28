using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessSolutions.Models
{
    public class OrderItem
    {
        [Key] public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 3)")] public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
