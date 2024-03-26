using GardenConsoleAPI.Business;
using GardenConsoleAPI.Business.Contracts;
using GardenConsoleAPI.Data.Models;
using GardenConsoleAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace GardenConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestPlantsDbContext dbContext;
        private IPlantsManager plantsManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestPlantsDbContext();
            this.plantsManager = new PlantsManager(new PlantsRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }


        //positive test
        [Test]
        public async Task AddPlantAsync_ShouldAddNewPlant()
        {
            // Arrange
            var newPlant = new Plant()
            {
                CatalogNumber = "12HP07PRFABC",
                Name = "Daisy",
                PlantType = "Flower",
                FoodType = "Non Edible Flower",
                Quantity = 800,
                IsEdible = false,
            };
            await plantsManager.AddAsync(newPlant);
            // Act
            var plantInDb = await dbContext.Plants.FirstOrDefaultAsync(p => p.CatalogNumber == newPlant.CatalogNumber);

            // Assert
            Assert.NotNull(plantInDb);
            Assert.That(plantInDb.Name, Is.EqualTo(newPlant.Name));
            Assert.That(plantInDb.PlantType, Is.EqualTo(newPlant.PlantType));
            Assert.That(plantInDb.FoodType, Is.EqualTo(newPlant.FoodType));
            Assert.That(plantInDb.Quantity, Is.EqualTo(newPlant.Quantity));
            Assert.That(plantInDb.IsEdible, Is.EqualTo(newPlant.IsEdible));
        }

        //Negative test
        [Test]
        public async Task AddPlantAsync_TryToAddPlantWithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var newPlant = new Plant()
            {
                CatalogNumber = "12HP07PRFABC",
                Name = new string('T', 100),
                PlantType = "Mars Flower",
                FoodType = "Non Edible Flower",
                Quantity = 56,
                IsEdible = false,
            };
            // Act and Assert
            var exception = Assert.ThrowsAsync<ValidationException>(() => plantsManager.AddAsync(newPlant));
            Assert.That(exception.Message, Is.EqualTo("Invalid plant!"));

        }

        [Test]
        public async Task DeletePlantAsync_WithValidCatalogNumber_ShouldRemovePlantFromDb()
        {
            // Arrange
            var flowerPlant = new Plant()
            {
                CatalogNumber = "12HP07PRFABC",
                Name = "Daisy",
                PlantType = "Flower",
                FoodType = "Non Edible Flower",
                Quantity = 800,
                IsEdible = false,
            };
            var herbsPlant = new Plant()
            {
                CatalogNumber = "14HP10PRFKFA",
                Name = "Basil",
                PlantType = "Herbs",
                FoodType = "Spices",
                Quantity = 150,
                IsEdible = true,
            };
            await plantsManager.AddAsync(flowerPlant);
            await plantsManager.AddAsync(herbsPlant);

            // Act
            await plantsManager.DeleteAsync(flowerPlant.CatalogNumber);
            var result = dbContext.Plants.Count();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(1));

            var plantInDb = dbContext.Plants.SingleOrDefault();
            Assert.IsNotNull(plantInDb);
            Assert.That(plantInDb.CatalogNumber, Is.EqualTo(herbsPlant.CatalogNumber));
            Assert.That(plantInDb.Name, Is.EqualTo("Basil"));
            Assert.That(plantInDb.PlantType, Is.EqualTo("Herbs"));
            Assert.That(plantInDb.FoodType, Is.EqualTo("Spices"));
            Assert.That(plantInDb.Quantity, Is.EqualTo(150));
            Assert.That(plantInDb.IsEdible, Is.EqualTo(true));

        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public async Task DeletePlantAsync_TryToDeleteWithNullOrWhiteSpaceCatalogNumber_ShouldThrowException(string invalidCatalogNumber)
        {
            // Arrange

            // Act and Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => plantsManager.DeleteAsync(invalidCatalogNumber));
            Assert.That(exception.Message, Is.EqualTo("Catalog number cannot be empty."));
            
        }

        [Test]
        public async Task GetAllAsync_WhenPlantsExist_ShouldReturnAllPlants()
        {
            // Arrange
            var flowerPlant = new Plant()
            {
                CatalogNumber = "12HP07PRFABC",
                Name = "Daisy",
                PlantType = "Flower",
                FoodType = "Non Edible Flower",
                Quantity = 800,
                IsEdible = false,
            };
            var herbsPlant = new Plant()
            {
                CatalogNumber = "14ZP10PRFKFA",
                Name = "Basil",
                PlantType = "Herbs",
                FoodType = "Spices",
                Quantity = 150,
                IsEdible = true,
            };
            var treesPlant = new Plant()
            {
                CatalogNumber = "17AP15PRDBYN",
                Name = "Peach",
                PlantType = "Trees",
                FoodType = "Fruit",
                Quantity = 350,
                IsEdible = true,
            };
            await plantsManager.AddAsync(flowerPlant);
            await plantsManager.AddAsync(herbsPlant);
            await plantsManager.AddAsync(treesPlant);

            // Act
            var result = await plantsManager.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(3));

            var lastPlantInDb = result.Last();
            Assert.That(lastPlantInDb.CatalogNumber, Is.EqualTo(treesPlant.CatalogNumber));
            Assert.That(lastPlantInDb.Name, Is.EqualTo(treesPlant.Name));
            Assert.That(lastPlantInDb.PlantType, Is.EqualTo(treesPlant.PlantType));
            Assert.That(lastPlantInDb.FoodType, Is.EqualTo(treesPlant.FoodType));
            Assert.That(lastPlantInDb.Quantity, Is.EqualTo(treesPlant.Quantity));
            Assert.That(lastPlantInDb.IsEdible, Is.EqualTo(treesPlant.IsEdible));
        }

        [Test]
        public async Task GetAllAsync_WhenNoPlantsExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => plantsManager.GetAllAsync());
            Assert.That(exception.Message, Is.EqualTo("No plant found."));
        }

        [Test]
        public async Task SearchByFoodTypeAsync_WithExistingFoodType_ShouldReturnMatchingPlants()
        {
            // Arrange
            var herbsPlant = new Plant()
            {
                CatalogNumber = "14ZP10PRFKFA",
                Name = "Basil",
                PlantType = "Herbs",
                FoodType = "Spices",
                Quantity = 150,
                IsEdible = true,
            };
            var treesPlant = new Plant()
            {
                CatalogNumber = "17AP15PRDBYN",
                Name = "Peach",
                PlantType = "Trees",
                FoodType = "Fruit",
                Quantity = 350,
                IsEdible = true,
            };
            var fruitPlant = new Plant()
            {
                CatalogNumber = "18AY18CHERRY",
                Name = "Cherry",
                PlantType = "Trees",
                FoodType = "Fruit",
                Quantity = 350,
                IsEdible = true,
            };
            await plantsManager.AddAsync(herbsPlant);
            await plantsManager.AddAsync(treesPlant);
            await plantsManager.AddAsync(fruitPlant);

            // Act
            var result = await plantsManager.SearchByFoodTypeAsync(treesPlant.FoodType);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            var plantInDb = result.First();
            Assert.That(plantInDb.CatalogNumber, Is.EqualTo(treesPlant.CatalogNumber));
            Assert.That(plantInDb.Name, Is.EqualTo(treesPlant.Name));
            Assert.That(plantInDb.PlantType, Is.EqualTo(treesPlant.PlantType));
            Assert.That(plantInDb.FoodType, Is.EqualTo(treesPlant.FoodType));
            Assert.That(plantInDb.Quantity, Is.EqualTo(treesPlant.Quantity));
            Assert.That(plantInDb.IsEdible, Is.EqualTo(treesPlant.IsEdible));
        }

        [Test]
        public async Task SearchByFoodTypeAsync_WithNonExistingFoodType_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            const string nonExistingFoodType = "non-existing-food-type";

            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => plantsManager.SearchByFoodTypeAsync(nonExistingFoodType));
            Assert.That(exception.Message, Is.EqualTo("No plant found with the given food type."));
        }

        [Test]
        public async Task GetSpecificAsync_WithValidCatalogNumber_ShouldReturnPlant()
        {
            
            // Arrange
            var flowerPlant = new Plant()
            {
                CatalogNumber = "12HP07PRFABC",
                Name = "Daisy",
                PlantType = "Flower",
                FoodType = "Non Edible Flower",
                Quantity = 800,
                IsEdible = false,
            };
            var herbsPlant = new Plant()
            {
                CatalogNumber = "14HP10PRFKFA",
                Name = "Basil",
                PlantType = "Herbs",
                FoodType = "Spices",
                Quantity = 150,
                IsEdible = true,
            };
            await plantsManager.AddAsync(flowerPlant);
            await plantsManager.AddAsync(herbsPlant);

            // Act
            var result = await plantsManager.GetSpecificAsync(flowerPlant.CatalogNumber);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo("Daisy"));
            Assert.That(result.PlantType, Is.EqualTo("Flower"));
            Assert.That(result.FoodType, Is.EqualTo("Non Edible Flower"));
            Assert.That(result.Quantity, Is.EqualTo(800));
            Assert.That(result.IsEdible, Is.EqualTo(false));
        }

        [TestCase("14HP10P_KJAY")]
        [TestCase("14HP10P")]
        [TestCase("123456789012")]
        [TestCase("ABCDEFJHIJKL")]
        [TestCase("@#$%^&*()_+-")]
        public async Task GetSpecificAsync_WithInvalidCatalogNumber_ShouldThrowKeyNotFoundException(string invalidCatalogNumber)
        {
            // Arrange

            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => plantsManager.GetSpecificAsync(invalidCatalogNumber));
            Assert.That(exception.Message, Is.EqualTo($"No plant found with catalog number: {invalidCatalogNumber}"));

        }

        [Test]
        public async Task UpdateAsync_WithValidPlant_ShouldUpdatePlant()
        {
            // Arrange
            var flowerPlant = new Plant()
            {
                CatalogNumber = "23MJ09LILIUM",
                Name = "Lilium",
                PlantType = "Flower",
                FoodType = "Non Edible Flower",
                Quantity = 130,
                IsEdible = false,
            };

            await plantsManager.AddAsync(flowerPlant);
            var updatedPlant = flowerPlant;
            updatedPlant.PlantType = "Nice smelling Lilium";
            updatedPlant.Quantity = 370;

            // Act

            await plantsManager.UpdateAsync(updatedPlant);

            // Assert
            var plantInDb = await dbContext.Plants.FirstOrDefaultAsync(p => p.CatalogNumber == updatedPlant.CatalogNumber);

            Assert.IsNotNull(plantInDb);
            Assert.That(plantInDb.Name, Is.EqualTo(updatedPlant.Name));
            Assert.That(plantInDb.PlantType, Is.EqualTo(updatedPlant.PlantType));
            Assert.That(plantInDb.FoodType, Is.EqualTo(updatedPlant.FoodType));
            Assert.That(plantInDb.Quantity, Is.EqualTo(updatedPlant.Quantity));
            Assert.That(plantInDb.IsEdible, Is.EqualTo(updatedPlant.IsEdible));
        }

        [Test]
        public async Task UpdateAsync_WithInvalidPlant_ShouldThrowValidationException()
        {
            // Arrange

            // Act and Assert
            var exception = Assert.ThrowsAsync<ValidationException>(() => plantsManager.UpdateAsync(new Plant()));
            Assert.That(exception.Message, Is.EqualTo("Invalid plant!"));
        }
    }
}
