using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab.service.Interfaces;
using Microsoft.Extensions.Logging;
using lab.infrastructure.data.Repositories.Interfaces;
using lab.infrastructure.data.Models;
using lab.domain.Models;
using AutoMapper;

namespace lab.service.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(
            ILoggerFactory loggerFactory,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger(this.GetType());
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductModel> AddAsync(ProductModel product)
        {
            _logger.LogInformation("Iniciando -> AddAsync");

            var productData = _mapper.Map<ProductDataModel>(product);

            productData = await _productRepository.AddAsync(productData);

            product = _mapper.Map<ProductModel>(productData);

            _logger.LogInformation("Finalizando -> AddAsync");
            
            return product;
        }

        public async Task<IList<ProductModel>> GetByNameAsync(string productName)
        {
            _logger.LogInformation("Iniciando -> GetByNameAsync");

            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentException("O parâmetro productName precisa ser preenchido com o nome do produto", "productName");
            }

            var productsData = await _productRepository.GetByNameAsync(productName);

            var products = _mapper.Map<IList<ProductModel>>(productsData);

            _logger.LogInformation("Finalizando -> GetByNameAsync");

            return products;
        }
        public async Task<IList<ProductModel>> GetByCodeAsync(string productCode)
        {
            _logger.LogInformation("Iniciando -> GetByCodeAsync");

            if (string.IsNullOrEmpty(productCode))
            {
                throw new ArgumentException("O parâmetro productCode precisa ser preenchido com o código do produto", "productCode");
            }

            var productsData = await _productRepository.GetByCodeAsync(productCode);

            var products = _mapper.Map<IList<ProductModel>>(productsData);

            _logger.LogInformation("Finalizando -> GetByCodeAsync");

            return products;
        }

        public async Task<ProductModel> GetByIdAsync(string productId)
        {

            _logger.LogInformation("Iniciando -> GetByIdAsync");

            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("O parâmetro productId precisa ser informado", "productId");
            }

            var productData = await _productRepository.GetByIdAsync(productId);

            var product = _mapper.Map<ProductModel>(productData);

            _logger.LogInformation("Finalizando -> GetByIdAsync");

            return product;
        }
    }
}