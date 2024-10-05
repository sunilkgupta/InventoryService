using Inventory.Business.Interfaces;
using Inventory.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Business.Implementation
{
    public class InventoryBusiness : IInventoryBusiness
    {
        private readonly InventoryAPIContext _context;
        private readonly ILogger<InventoryBusiness> _logger;
        public InventoryBusiness(InventoryAPIContext context, ILogger<InventoryBusiness> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Delete Inventory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteInventory(Guid id)
        {
            _logger.LogInformation("Inventory deleting, Id is {0}", id);
            try
            {
                var Inventory = await _context.Inventory.FindAsync(id);
                if (Inventory == null)
                {
                    _logger.LogWarning("Inventory Id is {0} null", id);
                    return false;
                }
                _context.Inventory.Remove(Inventory);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Inventory deleted successfully, Id is {0}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw;
            }
        }

        /// <summary>
        /// Get all Inventorys
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Common.Entities.Inventory>> GetInventories()
        {
            _logger.LogInformation("All Inventorys requested");
            try
            {
                var result = await _context.Inventory.ToListAsync();
                if (result == null || result.Count == 0)
                {
                    _logger.LogWarning("No Inventorys exists!");
                    return new List<Common.Entities.Inventory>();
                }
                return await _context.Inventory.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw;
            }
        }

        /// <summary>
        /// Get Inventorys by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Common.Entities.Inventory?> GetInventoryById(Guid id)
        {
            _logger.LogInformation("Inventory requested: Id is {0}", id);
            try
            {
                var Inventory = await _context.Inventory.FindAsync(id);

                if (Inventory == null)
                {
                    _logger.LogWarning("No Inventorys with this {0} exists!", id);
                    return null;
                }
                return Inventory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw;
            }
        }

        /// <summary>
        /// Create new Inventory
        /// </summary>
        /// <param name="Inventory"></param>
        /// <returns></returns>
        public async Task<Common.Entities.Inventory?> CreateInventory(Common.Entities.Inventory Inventory)
        {
            _logger.LogInformation("New Inventory creating..");
            try
            {
                _context.Inventory.Add(Inventory);
                await _context.SaveChangesAsync();
                return Inventory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw;
            }
        }

        /// <summary>
        /// Update existing Inventory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Inventory"></param>
        /// <returns></returns>
        public async Task UpdateInventory(Guid id, Common.Entities.Inventory Inventory)
        {
            _logger.LogInformation("Inventory updating: Id is {0}", id);
            try
            {
                _context.Entry(Inventory).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var result = await GetInventoryById(id);
                if (result == null)
                {
                    _logger.LogError(ex, "Db Update Concurrency Exception occurred as Id is missing: {0}", id);
                    throw;
                }
                else
                {
                    _logger.LogError(ex, "An error occurred while processing the request.");
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw;
            }
        }
    }
}
