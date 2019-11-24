using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using lab.service.Services;
using Microsoft.Extensions.Logging.Abstractions;
using lab.domain.Models;
using lab.domain.Interfaces.Services;

namespace lab.service.test.Tests
{
    
    public class ProductServiceTest
    {
        private readonly IServiceCollection _serviceCollection = new ServiceCollection();
        private readonly IProductService _productService;

        public ProductServiceTest()
        {
            _productService = new ProductService(
                new NullLoggerFactory(),
                null,
                null
            ); 
        }

        [Fact]
        public async Task GetByNameAsync_test()
        {
            IList<ProductModel> products = await _productService.GetByNameAsync("Ração");

            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task GetByCodeAsync_test()
        {
            IList<ProductModel> products = await _productService.GetByCodeAsync("001");

            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task GetByIdAsync_test()
        {
            ProductModel product = await _productService.GetByIdAsync(string.Empty);

            Assert.NotNull(product);
        }
    }
}
