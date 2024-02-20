using LibroConsoleAPI.Business;
using LibroConsoleAPI.Data.Models;
using LibroConsoleAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibroConsoleAPI.IntegrationTests.XUnit
{
    public class DeleteBookMethod : IClassFixture<BookManagerFixture>
    {
        private readonly BookManager _bookManager;
        private readonly TestLibroDbContext _dbContext;

        public DeleteBookMethod(BookManagerFixture fixture)
        {
            _bookManager = (BookManager?)fixture.BookManager;
            _dbContext = fixture.DbContext;
           
        }


        [Fact]
        public async Task DeleteBookAsync_WithValidISBN_1_ShouldRemoveBookFromDb()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "Test Book",
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            await _bookManager.AddAsync(newBook);

            // Act
            await _bookManager.DeleteAsync(newBook.ISBN);

            // Assert
            var bookInDb = await _dbContext.Books.FirstOrDefaultAsync();
            Assert.Null(bookInDb);

        }

        [Fact]
        public async Task DeleteBookAsync_WithValidISBN_2_ShouldRemoveBookFromDb()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);
            // Act
            await _bookManager.DeleteAsync("9780143039655");
            // Assert
            var bookInDb = _dbContext.Books.ToList();
            Assert.Equal(9, bookInDb.Count);
            Assert.True(bookInDb.Any());

        }

        [Fact]
        public async Task DeleteBookAsync_TryToDeleteWithNullISBN_ShouldThrowException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);

            // Act
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _bookManager.DeleteAsync(""));

            // Assert
            Assert.Equal("ISBN cannot be empty.", exception.Result.Message);
            var bookInDb = _dbContext.Books.ToList();
            Assert.Equal(10, bookInDb.Count);
        }

        [Fact]
        public async Task DeleteBookAsync_TryToDeleteWithWhiteSpaceISBN_ShouldThrowException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);

            // Act
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _bookManager.DeleteAsync(" "));

            // Assert
            Assert.Equal("ISBN cannot be empty.", exception.Result.Message);
            var bookInDb = _dbContext.Books.ToList();
            Assert.Equal(10, bookInDb.Count);
        }

        [Fact]
        public async Task DeleteBookAsync_TryToDeleteWithUnKnownISBN_ShouldThrowException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);

            // Act
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _bookManager.DeleteAsync("9780143039667"));

            // Assert
            //Assert.Equal("Value cannot be null.", exception.Result.Message);
            var bookInDb = _dbContext.Books.ToList();
            Assert.Equal(10, bookInDb.Count);
        }


    }
}
