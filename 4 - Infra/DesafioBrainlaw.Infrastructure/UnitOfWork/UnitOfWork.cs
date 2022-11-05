using DesafioBrainlaw.Domain.Interfaces.UnitOfWork;
using DesafioBrainlaw.Infrastructure.Context;

namespace DesafioBrainlaw.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields

        private readonly DesafioBrainlawContext _context;

        #endregion Private Fields

        #region Public Constructors

        public UnitOfWork(DesafioBrainlawContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<bool> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                var ex = e.Message;
                throw;
            }
        }

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Public Methods
    }
}