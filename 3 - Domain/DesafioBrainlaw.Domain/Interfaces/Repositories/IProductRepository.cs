using DesafioBrainlaw.Domain.Entities;

namespace DesafioBrainlaw.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IDisposable
    {
        #region Public Methods

        Task<List<Product>> GetAllAsync();

        Task<Product> GetAsync(Guid id);

        Task<Product> GetByNameAsync(string name);

        Task AddAsync(Product product);

        void Update(Product product);

        Task DeleteAsync(Guid id);

        #endregion Public Methods
    }
}