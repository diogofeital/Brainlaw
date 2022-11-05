namespace DesafioBrainlaw.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region Public Methods

        Task<bool> CommitAsync();

        #endregion Public Methods
    }
}