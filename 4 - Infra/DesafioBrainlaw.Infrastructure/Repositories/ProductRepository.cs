using DesafioBrainlaw.Domain.Entities;
using DesafioBrainlaw.Domain.Interfaces.Repositories;
using DesafioBrainlaw.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioBrainlaw.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        #region Private Fields

        private readonly DbSet<Product> _product;
        private readonly DesafioBrainlawContext _db;

        #endregion Private Fields

        #region Public Constructors

        public ProductRepository(DesafioBrainlawContext db)
        {
            _db = db;
            _product = _db.Set<Product>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<List<Product>> GetAllAsync()
        {
            return await _product
                .Where(x => x.IsDeleted == false)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _product
                .FirstOrDefaultAsync(p => p.IsDeleted == false && p.Id == id);
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            return await _product
                .FirstOrDefaultAsync(d => d.IsDeleted == false && d.Name.ToLower().Contains(name.ToLower()));
        }

        public async Task AddAsync(Product product)
        {
            await _product.AddAsync(product);
        }

        public void Update(Product product)
        {
            _product.Update(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await GetAsync(id);

            product.Delete();

            _product.Update(product);
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods
    }
}