using Ecom.core.Entities.Product;
using Ecom.core.Interfaces;
using Ecom.Infrastructture.Data;

namespace Ecom.Infrastructture.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
