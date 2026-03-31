using E_CommerceOrderManagementAPI.Application.Interfaces;
using E_CommerceOrderManagementAPI.Domain.Entities;
using E_CommerceOrderManagementAPI.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceOrderManagementAPI.Infrastructure.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly IAppDbContext _dbContext;
        public ProductRepository(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product?> GetProductByIdAsync(Guid productId)
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(f => f.ProductID == productId) ?? throw new DomainException($"Product with id {productId} not found.");

            return product;
        }
    }
}
