using Microsoft.EntityFrameworkCore;
using ProductConsoleAPI.Business;
using ProductConsoleAPI.Business.Contracts;
using ProductConsoleAPI.Data.Models;
using ProductConsoleAPI.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace ProductConsoleAPI.IntegrationTests.NUnit
{
    public  class IntegrationTests
    {
        private TestProductsDbContext dbContext;
        private IProductsManager productsManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestProductsDbContext();
            this.productsManager = new ProductsManager(new ProductsRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }


        //positive test
        [Test]
        public async Task AddProductAsync_ShouldAddNewProduct()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = 1.25m,
                Quantity = 100,
                Description = "Anything for description"
            };

            await productsManager.AddAsync(newProduct);

            var dbProduct = await this.dbContext.Products.FirstOrDefaultAsync(p => p.ProductCode == newProduct.ProductCode);

            Assert.NotNull(dbProduct);
            Assert.AreEqual(newProduct.ProductName, dbProduct.ProductName);
            Assert.AreEqual(newProduct.Description, dbProduct.Description);
            Assert.AreEqual(newProduct.Price, dbProduct.Price);
            Assert.AreEqual(newProduct.Quantity, dbProduct.Quantity);
            Assert.AreEqual(newProduct.OriginCountry, dbProduct.OriginCountry);
            Assert.AreEqual(newProduct.ProductCode, dbProduct.ProductCode);
        }

        //Negative test
        [Test]
        public async Task AddProductAsync_TryToAddProductWithInvalidCredentials_ShouldThrowException()
        {
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "TestProduct",
                ProductCode = "AB12C",
                Price = -1m,
                Quantity = 100,
                Description = "Anything for description"
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await productsManager.AddAsync(newProduct));
            var actual = await dbContext.Products.FirstOrDefaultAsync(c => c.ProductCode == newProduct.ProductCode);

            Assert.IsNull(actual);
            Assert.That(ex?.Message, Is.EqualTo("Invalid product!"));

        }

        [Test]
        public async Task DeleteProductAsync_WithValidProductCode_ShouldRemoveProductFromDb()
        {
            // Arrange
            var newProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "Table",
                ProductCode = "ABC123",
                Price = 102.50m,
                Quantity = 53,
                Description = "Table for all Homes"
            };

            await productsManager.AddAsync(newProduct);
            // Act
            await productsManager.DeleteAsync(newProduct.ProductCode);
            
            // Assert
            var productInDb = await dbContext.Products.FirstOrDefaultAsync(p => p.ProductCode == newProduct.ProductCode);
            Assert.IsNull(productInDb);
        }

        [Test]
        public async Task DeleteProductAsync_TryToDeleteWithNullOrWhiteSpaceProductCode_ShouldThrowException()
        {
            // Arrange

            // Act and Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => productsManager.DeleteAsync(null));
            Assert.That(exception.Message, Is.EqualTo("Product code cannot be empty."));
        }

        [Test]
        public async Task GetAllAsync_WhenProductsExist_ShouldReturnAllProducts()
        {
            // Arrange
            var firstProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "Table",
                ProductCode = "ABC123",
                Price = 102.50m,
                Quantity = 53,
                Description = "Table for all Homes"
            };
            var secondProduct = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "Chair",
                ProductCode = "FGA987",
                Price = 57.6m,
                Quantity = 256,
                Description = "Chair for all Homes"
            };
            await productsManager.AddAsync(firstProduct);
            await productsManager.AddAsync(secondProduct);
            // Act
             var result = await productsManager.GetAllAsync();
            // Assert
            Assert.IsTrue(result.Any());
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));

            var firstInDb = dbContext.Products.First(f => f.ProductName == firstProduct.ProductName);
            //var firstInDb = result.First();
            Assert.That(firstInDb.OriginCountry, Is.EqualTo(firstProduct.OriginCountry));
            Assert.That(firstInDb.ProductName, Is.EqualTo(firstProduct.ProductName));
            Assert.That(firstInDb.ProductCode, Is.EqualTo(firstProduct.ProductCode));
            Assert.That(firstInDb.Price, Is.EqualTo(firstProduct.Price));
            Assert.That(firstInDb.Quantity, Is.EqualTo(firstProduct.Quantity));
            Assert.That(firstInDb.Description, Is.EqualTo(firstProduct.Description));
           
        }

        [Test]
        public async Task GetAllAsync_WhenNoProductsExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => productsManager.GetAllAsync());
            Assert.That(exception.Message, Is.EqualTo("No product found."));
        }

        [Test]
        public async Task SearchByOriginCountry_WithExistingOriginCountry_ShouldReturnMatchingProducts()
        {
            // Arrange
            var productAUS = new Product()
            {
                OriginCountry = "Australia",
                ProductName = "Sofa",
                ProductCode = "AUS0912",
                Price = 1005.67m,
                Quantity = 74,
                Description = "Sofa for all Homes"
            };
            var productBG1 = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "Deskboard",
                ProductCode = "BG0123",
                Price = 82.45m,
                Quantity = 230,
                Description = "Deskboar for all Schools"
            };
            var productGR = new Product()
            {
                OriginCountry = "Greece",
                ProductName = "FishingBoard",
                ProductCode = "GR0567",
                Price = 356.78m,
                Quantity = 136,
                Description = "FishingBoard for all see feel people"
            };
            var productBG2 = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "Screen",
                ProductCode = "BG0124",
                Price = 98.35m,
                Quantity = 128,
                Description = "Screen for all Offices"
            };


            await productsManager.AddAsync(productAUS);
            await productsManager.AddAsync(productBG1);
            await productsManager.AddAsync(productGR);
            await productsManager.AddAsync(productBG2);

            // Act
            var result = await productsManager.SearchByOriginCountry("Bulgaria");
            
            // Assert
          
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            
            var productInDb = result.First();
            var anotherProductInDb = result.Last();
            Assert.That(productInDb.OriginCountry, Is.EqualTo(productBG1.OriginCountry));
            Assert.That(anotherProductInDb.OriginCountry, Is.EqualTo(productBG2.OriginCountry));
           
            
        }

        [Test]
        public async Task SearchByOriginCountryAsync_WithNonExistingOriginCountry_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var nontExistingCountry = "TestCountry";
            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => productsManager.SearchByOriginCountry(nontExistingCountry));
            Assert.That(exception.Message, Is.EqualTo("No product found with the given first name."));
        }

        [Test]
        public async Task GetSpecificAsync_WithValidProductCode_ShouldReturnProduct()
        {
            // Arrange
            var productBG1 = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "Deskboard",
                ProductCode = "BG0123",
                Price = 82.45m,
                Quantity = 230,
                Description = "Deskboar for all Schools"
            };
            var productGR = new Product()
            {
                OriginCountry = "Greece",
                ProductName = "FishingBoard",
                ProductCode = "GR0567",
                Price = 356.78m,
                Quantity = 136,
                Description = "FishingBoard for all see feel people"
            };
            await productsManager.AddAsync(productBG1);
            await productsManager.AddAsync(productGR);
            // Act
            var result = await productsManager.GetSpecificAsync(productGR.ProductCode);
            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.OriginCountry, Is.EqualTo(productGR.OriginCountry));
            Assert.That(result.ProductName, Is.EqualTo(productGR.ProductName));
            Assert.That(result.ProductCode, Is.EqualTo(productGR.ProductCode));
            Assert.That(result.Price, Is.EqualTo(productGR.Price));
            Assert.That(result.Quantity, Is.EqualTo(productGR.Quantity));
            Assert.That(result.Description, Is.EqualTo(productGR.Description));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidProductCode_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            const string invalidProductCode = "12";
            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => productsManager.GetSpecificAsync(invalidProductCode));
            Assert.That(exception.Message, Is.EqualTo($"No product found with product code: {invalidProductCode}"));
        }

        [Test]
        public async Task UpdateAsync_WithValidProduct_ShouldUpdateProduct()
        {
            // Arrange
            // Arrange
            var productBG1 = new Product()
            {
                OriginCountry = "Bulgaria",
                ProductName = "Deskboard",
                ProductCode = "BG0123",
                Price = 82.45m,
                Quantity = 230,
                Description = "Deskboar for all Schools"
            };
            await productsManager.AddAsync(productBG1);
            var updatedProduct = productBG1;
            updatedProduct.ProductCode = "BG666";
            updatedProduct.Price = 93.33m;
            // Act
            await productsManager.UpdateAsync(updatedProduct);
            // Assert
            var productInDb = await dbContext.Products.FirstOrDefaultAsync(p => p.ProductName == updatedProduct.ProductName);

            Assert.That(productInDb.OriginCountry, Is.EqualTo(updatedProduct.OriginCountry));
            Assert.That(productInDb.ProductCode, Is.EqualTo(updatedProduct.ProductCode));
            Assert.That(productInDb.Price, Is.EqualTo(updatedProduct.Price));
            Assert.That(productInDb.Quantity, Is.EqualTo(updatedProduct.Quantity));
            Assert.That(productInDb.Description, Is.EqualTo(updatedProduct.Description));
        }

        [Test]
        public async Task UpdateAsync_WithInvalidProduct_ShouldThrowValidationException()
        {
            // Arrange
            var newProduct = new Product();
            // Act and Assert
            var exception = Assert.ThrowsAsync<ValidationException>(() => productsManager.UpdateAsync(newProduct));
            Assert.That(exception.Message, Is.EqualTo("Invalid prduct!"));
        }
    }
}
