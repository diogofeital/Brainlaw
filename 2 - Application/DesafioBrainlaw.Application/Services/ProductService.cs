using DesafioBrainlaw.Application.Interfaces;
using DesafioBrainlaw.Application.Models.Common;
using DesafioBrainlaw.Application.Models.Product.Request;
using DesafioBrainlaw.Application.Models.Product.Response;
using DesafioBrainlaw.Domain.Entities;
using DesafioBrainlaw.Domain.Interfaces.Repositories;
using DesafioBrainlaw.Domain.Interfaces.UnitOfWork;
using DesafioBrainlaw.Domain.Shared.Interface.Notification;
using DesafioBrainlaw.Domain.Shared.Notifications;

namespace DesafioBrainlaw.Application.Services
{
    public class ProductService : AppService, IProductService
    {
        #region Private Fields

        private readonly INotifier _notifier;
        private readonly IProductRepository _productRepository;

        #endregion Private Fields

        #region Public Constructors

        public ProductService(IUnitOfWork unitOfWork,
            INotifier notifier,
            IProductRepository productRepository) : base(unitOfWork, notifier)
        {
            _notifier = notifier;
            _productRepository = productRepository;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IAppServiceResponse> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            if (products.Any() is false)
            {
                Notify("Não existe produto cadastrado.");

                return await Task.FromResult(new AppServiceResponse<ICollection<Notification>>(_notifier.GetAllNotifications(), "Erro ao obter produto.", false));
            }

            var response = new List<ProductResponseModel>();

            foreach (var product in products)
            {
                response.Add(new ProductResponseModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity
                });
            }

            return await Task.FromResult(new AppServiceResponse<List<ProductResponseModel>>(response, "Produto obtido com sucesso.", true));
        }

        public async Task<IAppServiceResponse> Get(Guid id)
        {
            var product = await _productRepository.GetAsync(id);

            if (product.Any())
            {
                Notify("Produto não encontrado");

                return await Task.FromResult(new AppServiceResponse<ICollection<Notification>>(_notifier.GetAllNotifications(), "Erro ao obter produto.", false));
            }

            var response = new ProductResponseModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity
            };

            return await Task.FromResult(new AppServiceResponse<ProductResponseModel>(response, "Produto obtido com sucesso.", true));
        }

        public async Task<IAppServiceResponse> Add(ProductRequestModel request)
        {
            var hasProduct = await _productRepository.GetByNameAsync(request.Name);

            if (string.IsNullOrEmpty(hasProduct?.Id.ToString()) is false)
            {
                Notify("Produto já existe");

                return await Task.FromResult(new AppServiceResponse<ICollection<Notification>>(_notifier.GetAllNotifications(), "Erro ao adicionar produto.", false));
            }

            var product = new Product(request.Name, request.Description, request.Price, request.Quantity);

            if (product.IsValid())
                await _productRepository.AddAsync(product);
            else
            {
                Notify(product.ValidationResult);

                return await Task.FromResult(new AppServiceResponse<ICollection<Notification>>(_notifier.GetAllNotifications(), "Erro ao adicionar produto.", false));
            }

            if (await CommitAsync() is false)
            {
                Notify("Erro ao salvar dados");

                return await Task.FromResult(new AppServiceResponse<ICollection<Notification>>(_notifier.GetAllNotifications(), "Erro ao obter produto.", false));
            }

            return await Task.FromResult(new AppServiceResponse<object>(null, "Produto adicionado com sucesso.", true));
        }

        public async Task<IAppServiceResponse> Update(Guid id, ProductRequestModel request)
        {
            var product = await _productRepository.GetAsync(id);

            if (string.IsNullOrEmpty(product?.Id.ToString()))
            {
                Notify("Produto não encontrado.");

                return await Task.FromResult(new AppServiceResponse<ICollection<Notification>>(_notifier.GetAllNotifications(), "Erro ao atualizar produto.", false));
            }

            product.Update(request.Name, request.Description, request.Price, request.Quantity);

            if (product.IsValid())
                _productRepository.Update(product);
            else
            {
                Notify(product.ValidationResult);

                return await Task.FromResult(new AppServiceResponse<ICollection<Notification>>(_notifier.GetAllNotifications(), "Erro ao atualizar produto.", false));
            }

            if (await CommitAsync() is false)
            {
                Notify("Erro ao salvar dados");

                return await Task.FromResult(new AppServiceResponse<ICollection<Notification>>(_notifier.GetAllNotifications(), "Erro ao obter produto.", false));
            }

            return await Task.FromResult(new AppServiceResponse<object>(null, "Produto atualizado com sucesso.", true));
        }

        public async Task<IAppServiceResponse> Delete(Guid id)
        {
            var hasProduct = await _productRepository.GetAsync(id);

            if (string.IsNullOrEmpty(hasProduct?.Id.ToString()))
            {
                Notify("Produto não encontrado");

                return await Task.FromResult(new AppServiceResponse<ICollection<Notification>>(_notifier.GetAllNotifications(), "Erro ao excluir produto.", false));
            }

            hasProduct.Delete();

            _productRepository.Update(hasProduct);

            if (await CommitAsync() is false)
            {
                Notify("Erro ao excluir produto.");

                return await Task.FromResult(new AppServiceResponse<ICollection<Notification>>(_notifier.GetAllNotifications(), "Erro ao excluir produto.", false));
            }

            return await Task.FromResult(new AppServiceResponse<object>(null, "Produto deletado com sucesso.", true));
        }

        #endregion Public Methods
    }
}