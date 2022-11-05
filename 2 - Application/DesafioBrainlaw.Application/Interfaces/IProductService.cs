using DesafioBrainlaw.Application.Models.Product.Request;

namespace DesafioBrainlaw.Application.Interfaces
{
    public interface IProductService
    {
        #region Public Methods

        Task<IAppServiceResponse> Get(Guid id);
        Task<IAppServiceResponse> GetAllAsync();
        Task<IAppServiceResponse> Add(ProductRequestModel request);
        Task<IAppServiceResponse> Update(Guid id, ProductRequestModel request);
        Task<IAppServiceResponse> Delete(Guid id);

        #endregion Public Methods
    }
}