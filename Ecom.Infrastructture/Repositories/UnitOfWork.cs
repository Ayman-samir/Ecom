using AutoMapper;
using Ecom.core.Interfaces;
using Ecom.core.Services;
using Ecom.Infrastructture.Data;

namespace Ecom.Infrastructture.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageMandmentService _imageMangmentService;
        public ICategoryRepository CategoryRepository
        {
            get;
        }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository PhotoRepository { get; }
        public UnitOfWork(AppDbContext context, IMapper mapper, IImageMandmentService imageMangmentService)
        {
            _context = context;
            _mapper = mapper;
            _imageMangmentService = imageMangmentService;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context, _mapper, _imageMangmentService);
            PhotoRepository = new PhotoRepository(_context);

        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
