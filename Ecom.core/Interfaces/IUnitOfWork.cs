using Ecom.Infrastructture.Repositories;

namespace Ecom.core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IPhotoRepository PhotoRepository { get; }

        Task<int> CompleteAsync();
    }
}
