using ContactsConsoleAPI.Business;
using ContactsConsoleAPI.Business.Contracts;
using ContactsConsoleAPI.Data.Models;
using ContactsConsoleAPI.DataAccess;
using ContactsConsoleAPI.DataAccess.Contrackts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestContactDbContext dbContext;
        private IContactManager contactManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestContactDbContext();
            this.contactManager = new ContactManager(new ContactRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }


        //positive test
        [Test]
        public async Task AddContactAsync_ShouldAddNewContact()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var dbContact = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == newContact.Contact_ULID);

            Assert.NotNull(dbContact);
            Assert.AreEqual(newContact.FirstName, dbContact.FirstName);
            Assert.AreEqual(newContact.LastName, dbContact.LastName);
            Assert.AreEqual(newContact.Phone, dbContact.Phone);
            Assert.AreEqual(newContact.Email, dbContact.Email);
            Assert.AreEqual(newContact.Address, dbContact.Address);
            Assert.AreEqual(newContact.Contact_ULID, dbContact.Contact_ULID);
        }

        //Negative test
        [Test]
        public async Task AddContactAsync_TryToAddContactWithInvalidCredentials_ShouldThrowException()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "invalid_Mail", //invalid email
                Gender = "Male",
                Phone = "0889933779"
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await contactManager.AddAsync(newContact));
            var actual = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == newContact.Contact_ULID);

            Assert.IsNull(actual);
            Assert.That(ex?.Message, Is.EqualTo("Invalid contact!"));

        }

        [Test]
        public async Task DeleteContactAsync_WithValidULID_ShouldRemoveContactFromDb()
        {
            // Arrange
            var firstContact = new Contact()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Address = "Sofia, Bulgaria",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "ivan_ivanov@gmail.com",
                Gender = "Male",
                Phone = "0883456987"
            };
            
            await contactManager.AddAsync(firstContact);
          
            // Act
            await contactManager.DeleteAsync(firstContact.Contact_ULID);

            // Assert
            var contactInDb = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == firstContact.Contact_ULID);
            Assert.IsNull(contactInDb);
        }

        [Test]
        public async Task DeleteContactAsync_WithValidULID_ShouldRemoveContactFromDb_CaseWithTwoAddedContact()
        {
            // Arrange
            var firstContact = new Contact()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Address = "Sofia, Bulgaria",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "ivan_ivanov@gmail.com",
                Gender = "Male",
                Phone = "0883456987"
            };
            var secondContact = new Contact()
            {
                FirstName = "Petya",
                LastName = "Ivanova",
                Address = "Stara Zagora, Bulgaria",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "petya_Iv@gmail.com",
                Gender = "Female",
                Phone = "0881876543"
            };
            await contactManager.AddAsync(firstContact);
            await contactManager.AddAsync(secondContact);


            // Act
            await contactManager.DeleteAsync(firstContact.Contact_ULID);
            var result = dbContext.Contacts.Count();

            // Assert
            Assert.That(result, Is.EqualTo(1));

        }

        [Test]
        public async Task DeleteContactAsync_TryToDeleteWithNullULID_ShouldThrowException()
        {
            // Arrange

            // Act and Assert
            Assert.ThrowsAsync<ArgumentException>(() => contactManager.DeleteAsync(null));
        }


        [TestCase("")]
        [TestCase("   ")]
        public async Task DeleteContactAsync_TryToDeleteWithWhiteSpaceULID_ShouldThrowException(string invalidULID)
        {
            // Arrange
           
            // Act and Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => contactManager.DeleteAsync(invalidULID));
            Assert.That(exception.Message, Is.EqualTo("ULID cannot be empty."));
        }

        [Test]
        public async Task GetAllAsync_WhenContactsExist_ShouldReturnAllContacts()
        {
            // Arrange
            var firstContact = new Contact()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Address = "Sofia, Bulgaria",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "ivan_ivanov@gmail.com",
                Gender = "Male",
                Phone = "0883456987"
            };
            var secondContact = new Contact()
            {
                FirstName = "Petya",
                LastName = "Ivanova",
                Address = "Stara Zagora, Bulgaria",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "petya_Iv@gmail.com",
                Gender = "Female",
                Phone = "0881876543"
            };
            await contactManager.AddAsync(firstContact);
            await contactManager.AddAsync(secondContact);
            // Act
            var result = await contactManager.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));

            var firstContactInDb = result.First();
            Assert.That(firstContactInDb.FirstName, Is.EqualTo(firstContact.FirstName));
            Assert.That(firstContactInDb.LastName, Is.EqualTo(firstContact.LastName));
            Assert.That(firstContactInDb.Email, Is.EqualTo(firstContact.Email));
            Assert.That(firstContactInDb.Address, Is.EqualTo(firstContact.Address));
            Assert.That(firstContactInDb.Gender, Is.EqualTo(firstContact.Gender));
            Assert.That(firstContactInDb.Phone, Is.EqualTo(firstContact.Phone));
        }

        [Test]
        public async Task GetAllAsync_WhenNoContactsExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.GetAllAsync());
            Assert.That(exception.Message, Is.EqualTo("No contact found."));
        }

        [Test]
        public async Task SearchByFirstNameAsync_WithExistingFirstName_ShouldReturnMatchingContacts()
        {

            // Arrange
            var firstContact = new Contact()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Address = "Sofia, Bulgaria",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "ivan_ivanov@gmail.com",
                Gender = "Male",
                Phone = "0883456987"
            };
            var secondContact = new Contact()
            {
                FirstName = "Petya",
                LastName = "Ivanova",
                Address = "Stara Zagora, Bulgaria",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "petya_Iv@gmail.com",
                Gender = "Female",
                Phone = "0881876543"
            };
            await contactManager.AddAsync(firstContact);
            await contactManager.AddAsync(secondContact);

            // Act
            var result = await contactManager.SearchByFirstNameAsync(firstContact.FirstName);
            
            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            var contactInDb = result.First();
            Assert.That(contactInDb.FirstName,Is.EqualTo(firstContact.FirstName));
        }

        
        [Test]
        public async Task SearchByFirstNameAsync_WithNonExistingFirstName_ShouldThrowKeyNotFoundException()
        {
            //Arrange
            const string nonExistingFirstName = "Viola";
            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.SearchByFirstNameAsync(nonExistingFirstName));
            Assert.That(exception.Message, Is.EqualTo("No contact found with the given first name."));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task SearchByFirstNameAsync_WithNullOrWhiteSpaceFirstName_ShouldThrowKeyNotFoundException(string nonExistingFirstName)
        {

            // Act and Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => contactManager.SearchByFirstNameAsync(nonExistingFirstName));
            Assert.That(exception.Message, Is.EqualTo("First name cannot be empty."));
        }

        [Test]
        public async Task SearchByFirstNameAsync_WithNonExistingContact_ShouldThrowKeyNotFoundException()
        {
            var firstContact = new Contact()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Address = "Sofia, Bulgaria",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "ivan_ivanov@gmail.com",
                Gender = "Male",
                Phone = "0883456987"
            };
            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.SearchByFirstNameAsync(firstContact.FirstName));
            Assert.That(exception.Message, Is.EqualTo("No contact found with the given first name."));

        }


        [Test]
        public async Task SearchByLastNameAsync_WithExistingLastName_ShouldReturnMatchingContacts()
        {
            // Arrange
            var firstContact = new Contact()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Address = "Sofia, Bulgaria",
                Contact_ULID = "1ABC23456HH", 
                Email = "ivan_ivanov@gmail.com",
                Gender = "Male",
                Phone = "0883456987"
            };
            var secondContact = new Contact()
            {
                FirstName = "Petya",
                LastName = "Ivanova",
                Address = "Stara Zagora, Bulgaria",
                Contact_ULID = "1ABC23456HH", 
                Email = "petya_Iv@gmail.com",
                Gender = "Female",
                Phone = "0881876543"
            };
            await contactManager.AddAsync(firstContact);
            await contactManager.AddAsync(secondContact);

            // Act
            var result = await contactManager.SearchByLastNameAsync(secondContact.LastName);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            var contactInDb = result.First();
            Assert.That(contactInDb.LastName, Is.EqualTo(secondContact.LastName));
        }

        [Test]
        public async Task SearchByLastNameAsync_WithNonExistingLastName_ShouldThrowKeyNotFoundException()
        {

            //Arrange
            const string nonExistingLasttName = "Paychevich";
            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.SearchByLastNameAsync(nonExistingLasttName));
            Assert.That(exception.Message, Is.EqualTo("No contact found with the given last name."));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task SearchByLastNameAsync_WithNullOrWhiteSpaceLastName_ShouldThrowKeyNotFoundException(string nonExistingLastName)
        {

            // Act and Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => contactManager.SearchByLastNameAsync(nonExistingLastName));
            Assert.That(exception.Message, Is.EqualTo("Last name cannot be empty."));
        }

        [Test]
        public async Task SearchByLastNameAsync_WithNonExistingContact_ShouldThrowKeyNotFoundException()
        {
            var firstContact = new Contact()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Address = "Sofia, Bulgaria",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "ivan_ivanov@gmail.com",
                Gender = "Male",
                Phone = "0883456987"
            };
            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.SearchByLastNameAsync(firstContact.LastName));
            Assert.That(exception.Message, Is.EqualTo("No contact found with the given last name."));

        }


        [Test]
        public async Task GetSpecificAsync_WithValidULID_ShouldReturnContact()
        {
            // Arrange
            var firstContact = new Contact()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Address = "Sofia, Bulgaria",
                Contact_ULID = "1ABC23456HH", 
                Email = "ivan_ivanov@gmail.com",
                Gender = "Male",
                Phone = "0883456987"
            };
            var secondContact = new Contact()
            {
                FirstName = "Petya",
                LastName = "Ivanova",
                Address = "Stara Zagora, Bulgaria",
                Contact_ULID = "1ABC23456HH", 
                Email = "petya_Iv@gmail.com",
                Gender = "Female",
                Phone = "0881876543"
            };
            await contactManager.AddAsync(firstContact);
            await contactManager.AddAsync(secondContact);

            // Act
            var result = await contactManager.GetSpecificAsync(firstContact.Contact_ULID);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.FirstName, Is.EqualTo(firstContact.FirstName));
            Assert.That(result.LastName, Is.EqualTo(firstContact.LastName));
            Assert.That(result.Address, Is.EqualTo(firstContact.Address));
            Assert.That(result.Email, Is.EqualTo(firstContact.Email));
            Assert.That(result.Gender, Is.EqualTo(firstContact.Gender));
            Assert.That(result.Phone, Is.EqualTo(firstContact.Phone));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidULID_ShouldThrowKeyNotFoundException()
        {
            //Arrange
            const string invalidULID = "123_JK ";
            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.GetSpecificAsync(invalidULID));
            Assert.That(exception.Message, Is.EqualTo($"No contact found with ULID: {invalidULID}"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public async Task GetSpecificAsync_WithNullOrWhiteSpaceULID_ShouldReturnArgumentException(string invalidULID)
        {
            // Act and Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => contactManager.GetSpecificAsync(invalidULID));
            Assert.That(exception.Message, Is.EqualTo("ULID cannot be empty."));


        }

        [Test]
        public async Task UpdateAsync_WithValidContact_ShouldUpdateContact()
        {
            var firstContact = new Contact()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Address = "Sofia, Bulgaria",
                Contact_ULID = "1ABC23456HH",
                Email = "ivan_ivanov@gmail.com",
                Gender = "Male",
                Phone = "0883456987"
            };
           
            await contactManager.AddAsync(firstContact);
            var updatedContact = firstContact;
            updatedContact.Email = "vanka66@yahoo.com";
            updatedContact.Phone = "0773456932";

            // Act
            await contactManager.UpdateAsync(updatedContact);

            // Assert
            var contactInDb = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Contact_ULID == updatedContact.Contact_ULID);

            Assert.That(contactInDb.FirstName, Is.EqualTo(updatedContact.FirstName));
            Assert.That(contactInDb.LastName, Is.EqualTo(updatedContact.LastName));
            Assert.That(contactInDb.Address, Is.EqualTo(updatedContact.Address));
            Assert.That(contactInDb.Email, Is.EqualTo(updatedContact.Email));
            Assert.That(contactInDb.Gender, Is.EqualTo(updatedContact.Gender));
            Assert.That(contactInDb.Phone, Is.EqualTo(updatedContact.Phone));
            
        }

        [Test]
        public async Task UpdateAsync_WithInvalidContact_ShouldThrowValidationException()
        {
            // Arrange

            // Act and Assert
            var exception = Assert.ThrowsAsync<ValidationException>(() => contactManager.UpdateAsync(new Contact()));

            Assert.That(exception.Message, Is.EqualTo("Invalid contact!"));
        }
    }
}
