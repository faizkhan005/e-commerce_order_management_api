using E_CommerceOrderManagementAPI.Domain.Entities;

namespace E_CommerceOrderManagementAPI.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByIdAsync(Guid productId);
    }
}
