using Microsoft.EntityFrameworkCore;

namespace Inventory.API.Data
{
    public class InventoryAPIContext : DbContext
    {
        public InventoryAPIContext (DbContextOptions<InventoryAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Inventory.API.Models.Inventory> Inventory { get; set; } = default!;
    }
}
