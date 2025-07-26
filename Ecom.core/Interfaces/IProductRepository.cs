using Ecom.core.Dtos.Products;
using Ecom.core.Entities.Product;
using Ecom.core.Interfaces;
using Ecom.core.Sharing;

namespace Ecom.Infrastructture.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        // for future
        Task<IEnumerable<ProductDto>> GetAllAsync(ProductParams productParams);
        Task<bool> AddAsync(AddProductDto productDto);
        Task<bool> UpdateAsync(int id, UpdateProductDto updateProductDto);
        Task<bool> DeleteAsync(int id);
    }
    /*public interface IGenericRepository<T> where T : class
   {
       Task<IReadOnlyList<T>> GetAllAsync();
       Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
       Task<T> GetByIdAsync(int id);
       Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
       Task AddAsync(T entity);
       Task UpdateAsync(int id, T entity);
       Task DeleteAsync(int id);
   }*/
}
