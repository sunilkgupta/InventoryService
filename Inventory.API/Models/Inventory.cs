using System.ComponentModel.DataAnnotations;

namespace Inventory.API.Models
{
    public class Inventory
    {
        [Key]
        public Guid ItemId { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
