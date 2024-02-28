using NUnit.Framework;
using Moq;
using ItemManagementApp.Services;
using ItemManagementLib.Repositories;
using ItemManagementLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ItemManagement.Tests
{
    [TestFixture]
    public class ItemServiceTests
    {
        private ItemService _itemService;
        private Mock<IItemRepository> _mockItemRepository;
        

        [SetUp]
        public void Setup()
        {
            // Arrange: Create a mock instance of IItemRepository
            _mockItemRepository = new Mock<IItemRepository>();
            // Instantiate ItemService with the mocked repository
            _itemService = new ItemService(_mockItemRepository.Object);
        }

        


        [Test]
        public void AddItem_ShouldCallAddItemOnRepository()
        {
            //Arrange
            var item = new Item { Name = "Sofa" };
            _mockItemRepository.Setup(x => x.AddItem(It.IsAny<Item>()));

            //Act
            _itemService.AddItem(item.Name);

            //Assert
            _mockItemRepository.Verify(x => x.AddItem(It.IsAny<Item>()), Times.Once());
          
        }


        [Test]
        public void AddItem_ShouldReturnArgumentException_IfItemNameIsOverMaxLength()
        {
            //Arrange
            string invalidName = new string('S', 20);
            _mockItemRepository
                .Setup(x => x.AddItem(It.IsAny<Item>())).Throws<ArgumentException>();

            //Act and Assert

            Assert.Throws<ArgumentException>(() => _itemService.AddItem(invalidName));
            _mockItemRepository.Verify(x => x.AddItem(It.IsAny<Item>()), Times.Once());

        }

        [Test]
        public void AddItem_ShouldReturnArgumentException_IfItemNameIsWhiteSpace()
        {
            //Arrange
            string invalidName = " ";
            _mockItemRepository
                .Setup(x => x.AddItem(It.IsAny<Item>())).Throws<ArgumentException>();

            //Act and Assert

            Assert.Throws<ArgumentException>(() => _itemService.AddItem(invalidName));
            _mockItemRepository.Verify(x => x.AddItem(It.IsAny<Item>()), Times.Once());

        }



        [Test]
        public void GetAllItems_ShouldReturnAllItems()
        {
            //Arrange
            var items = new List<Item>()
            {
                new Item { Id = 1, Name = "Table"},
                new Item { Id = 2, Name = "Chair"}
            };
            _mockItemRepository.Setup(x => x.GetAllItems()).Returns(items);

            //Act
            var result = _itemService.GetAllItems();

            //Assert
            Assert.NotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            _mockItemRepository.Verify(x => x.GetAllItems(), Times.Once());
        }

        [Test]
        public void GetItemByI_ShouldReturnGetItemById_IfItemExists()
        {
            //Arrange
            var item = new Item { Id = 1, Name = "Vase" };
            _mockItemRepository.Setup(x => x.GetItemById(1)).Returns(item);
            //Act
            var result = _itemService.GetItemById(item.Id);

            //Assert
            Assert.NotNull(result);
            Assert.That(result.Name, Is.EqualTo(item.Name));
            _mockItemRepository.Verify(x => x.GetItemById(item.Id), Times.Once());

        }

        [Test]
        public void GetItemByI_ShouldReturnNull_IfItemNotExists()
        {
            //Arrange
            Item item = null;
            _mockItemRepository.Setup(x => x.GetItemById(It.IsAny<int>())).Returns(item);
            //Act
            var result = _itemService.GetItemById(123);

            //Assert
            Assert.Null(result);
            _mockItemRepository.Verify(x => x.GetItemById(It.IsAny<int>()), Times.Once());

        }


        [Test]
        public void UpdateItem_ShouldNotUpdateItem_IfItemDoseNotExists()
        {
            //Arrange
            var nonExistingId = 1;
            _mockItemRepository.Setup(x => x.GetItemById(nonExistingId)).Returns<Item>(null);
            _mockItemRepository.Setup(x => x.UpdateItem(It.IsAny<Item>()));
            //Act
            _itemService.UpdateItem(nonExistingId, "");
            //Assert
            _mockItemRepository.Verify(x => x.GetItemById(nonExistingId), Times.Once());
            _mockItemRepository.Verify(x => x.UpdateItem(It.IsAny<Item>()), Times.Never);

        }

        [Test]
        public void UpdateItem_ShouldThrowExeption_IfItemNameIsInvalid()
        {
            //Arrange
            var item = new Item { Id = 1, Name = "Test name" };
            _mockItemRepository.Setup(x => x.GetItemById(item.Id)).Returns(item);
            _mockItemRepository.Setup(x => x.UpdateItem(It.IsAny<Item>()))
                .Throws<ArgumentException>();
            //Act and Assert
            Assert.Throws<ArgumentException>(() => _itemService.UpdateItem(item.Id, new string('t', 25)));
            _mockItemRepository.Verify(x => x.GetItemById(item.Id), Times.Once());
            _mockItemRepository.Verify(x => x.UpdateItem(It.IsAny<Item>()), Times.Once());

        }


        [Test]
        public void UpdateItem_ShouldUpdateItem_IfItemNameIsValid()
        {
            //Arrange
            var item = new Item { Id = 1, Name = "Test name" };
            _mockItemRepository.Setup(x => x.GetItemById(item.Id)).Returns(item);
            _mockItemRepository.Setup(x => x.UpdateItem(It.IsAny<Item>()));

            //Act
            _itemService.UpdateItem(item.Id, "Updated");

            //Assert
            _mockItemRepository.Verify(x => x.GetItemById(item.Id), Times.Once());
            _mockItemRepository.Verify(x => x.UpdateItem(It.IsAny<Item>()), Times.Once());

        }

        [Test]
        public void DeleteItem_ShouldCallDeleteItemOnRepository()
        {
            //Arrang
            var itemId = 15;
            var item = new Item { Id = itemId,  Name = "Test" };
            _mockItemRepository.Setup(x => x.DeleteItem(itemId));
            //Act
            _itemService.DeleteItem(itemId);
            //Assert
            _mockItemRepository.Verify(x => x.DeleteItem(itemId), Times.Once());    



        }



        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("abcabcabcabc", false)]
        [TestCase("A", true)]
        [TestCase("Test name", true)]
        [TestCase("Test", true)]
        public void ValidateItemName_ShouldReturnValidName_IfItemNameIsValid(string name, bool isValid)
        {
           
            //Act
            var result = _itemService.ValidateItemName(name);
            //Assert
            Assert.That(result, Is.EqualTo(isValid));

        }
    }
}