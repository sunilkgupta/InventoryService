using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Business.Interfaces
{
    public interface IInventoryBusiness
    {
        Task<IEnumerable<Common.Entities.Inventory>> GetInventories();

        Task<Common.Entities.Inventory?> GetInventoryById(Guid id);

        Task UpdateInventory(Guid id, Common.Entities.Inventory inventory);

        Task<Common.Entities.Inventory?> CreateInventory(Common.Entities.Inventory inventory);

        Task<bool> DeleteInventory(Guid id);
    }
}
