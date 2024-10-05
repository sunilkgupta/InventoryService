using Microsoft.EntityFrameworkCore;

namespace Inventory.DataContext
{
    public class InventoryAPIContext : DbContext
    {
        public InventoryAPIContext(DbContextOptions<InventoryAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Common.Entities.Inventory> Inventory { get; set; } = default!;
    }
}
