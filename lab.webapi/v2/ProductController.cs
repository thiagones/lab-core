using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using lab.domain.Models;
using lab.domain.Models;
using lab.service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace lab.webapi.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService,
            IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<IActionResult> GetByIdAsync(string productId)
        {
            _logger.LogInformation($"Iniciando -> GetByIdAsync v.2.0");

            var product = await _productService.GetByIdAsync(productId);
            
            var productApi = _mapper.Map<domain.Models.Api.v2.ProductApiModel>(product);

            _logger.LogInformation($"Finalizando -> GetByIdAsync v.2.0");

            return new OkObjectResult(productApi);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string name, string code)
        {
            _logger.LogInformation($"Iniciando -> GetAsync v2.0");

            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(code))
            {
                return BadRequest("Deve ser informado o parâmetro \"name\" ou \"code\" do produto");
            }

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(code))
            {
                return BadRequest("Deve ser informado apenas um dos parâmetros");
            }

            IList<ProductModel> products = null;

            if (!string.IsNullOrEmpty(name))
            {
                products = await _productService.GetByNameAsync(name);
            }

            if (!string.IsNullOrEmpty(code))
            {
                products = await _productService.GetByCodeAsync(code);
            }

            var productsApi = _mapper.Map<IList<domain.Models.Api.v2.ProductApiModel>>(products);

            _logger.LogInformation($"Finalizando -> GetAsync v2.0");

            return new OkObjectResult(productsApi);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]domain.Models.Api.v2.ProductApiModel productApi)
        {
            _logger.LogInformation($"Iniciando -> PostAsync v2.0");

            ProductModel product = _mapper.Map<ProductModel>(productApi);

            product = await _productService.AddAsync(product);

            productApi = _mapper.Map<domain.Models.Api.v2.ProductApiModel>(product);

            _logger.LogInformation($"Finalizando -> PostAsync v2.0");

            return new CreatedResult(product.Id, productApi);
        }
        
    }
}
