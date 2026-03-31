using E_CommerceOrderManagementAPI.Application.Interfaces;
using E_CommerceOrderManagementAPI.Domain.Entities;
using E_CommerceOrderManagementAPI.Domain.Exceptions;

namespace E_CommerceOrderManagementAPI.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _productRepository;

        public InventoryService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            Product? product = await _productRepository.GetProductByIdAsync(productId);

            return product ?? throw new DomainException($"Product with ID {productId} not found");
        }
    }
}
