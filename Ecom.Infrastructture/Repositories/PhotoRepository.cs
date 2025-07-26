using Ecom.core.Entities.Product;
using Ecom.core.Interfaces;
using Ecom.Infrastructture.Data;

namespace Ecom.Infrastructture.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        private readonly AppDbContext _context;

        public PhotoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
