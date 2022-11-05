using System.Net;
using DesafioBrainlaw.Application.Interfaces;
using DesafioBrainlaw.Application.Models.Product.Request;
using DesafioBrainlaw.Domain.Shared.Interface.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBrainlaw.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(INotifier notifier, 
            IProductService productService) : base(notifier)
        {
            _productService = productService;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> GetAll()
        {
            var product = await _productService.GetAllAsync();

            return GenerateResponse(ValidOperation() is false ? HttpStatusCode.NotFound : HttpStatusCode.OK, product);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productService.Get(id);

            return GenerateResponse(ValidOperation() is false ? HttpStatusCode.NotFound : HttpStatusCode.OK, product);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductRequestModel request)
        {
            var product = await _productService.Add(request);

            return GenerateResponse(ValidOperation() is false ? HttpStatusCode.NotFound : HttpStatusCode.Created, product);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, ProductRequestModel request)
        {
            var product = await _productService.Update(id, request);

            return GenerateResponse(ValidOperation() is false ? HttpStatusCode.NotFound : HttpStatusCode.Created, product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.Delete(id);

            return GenerateResponse(ValidOperation() is false ? HttpStatusCode.NotFound : HttpStatusCode.Created, product);
        }
    }
}
