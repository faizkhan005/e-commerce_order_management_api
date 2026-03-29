using E_CommerceOrderManagementAPI.Domain.Entities;

namespace E_CommerceOrderManagementAPI.Application.Interfaces
{
    public interface IInventoryService
    {
        public Task<Product> GetProductByIdAsync(Guid productId);
    }
}
