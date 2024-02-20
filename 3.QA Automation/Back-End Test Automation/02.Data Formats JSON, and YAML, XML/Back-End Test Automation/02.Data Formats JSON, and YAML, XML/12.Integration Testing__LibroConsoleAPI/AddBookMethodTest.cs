using LibroConsoleAPI.Business;
using LibroConsoleAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibroConsoleAPI.IntegrationTests.XUnit
{
    public class AddBookMethodTest : IClassFixture<BookManagerFixture>
    {
        private readonly BookManager _bookManager;
        private readonly TestLibroDbContext _dbContext;

        public AddBookMethodTest(BookManagerFixture fixture)
        {
            _bookManager = (BookManager?)fixture.BookManager;
            _dbContext = fixture.DbContext;
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddBookAsync_ShouldAddBook()
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

            // Act
            await _bookManager.AddAsync(newBook);

            // Assert
            var bookInDb = await _dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == newBook.ISBN);
            Assert.NotNull(bookInDb);
            Assert.Equal("Test Book", bookInDb.Title);
            Assert.Equal("John Doe", bookInDb.Author);
        }

        [Fact]
        public async Task AddBookAsync_TryToAddBookWithInvalidTitle_ShouldThrowException()
        {
            // Arrange
            var newBook = new Book
            {
                Title = new string('A', 300),
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(newBook));

            // Assert
            Assert.Equal("Book is invalid.", exception.Message);
            var bookInDB = await _dbContext.Books.FirstOrDefaultAsync();
            Assert.Null(bookInDB);
        }

        [Fact]
        public async Task AddBookAsync_TryToAddBookWithInvalidAuthor_ShouldThrowException()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "New year Eve",
                Author = new string('A', 150),
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(newBook));

            // Assert
            Assert.Equal("Book is invalid.", exception.Message);
        }

        [Fact]
        public async Task AddBookAsync_TryToAddBookWithInvalidISBN_ShouldThrowException()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "New year Eve",
                Author = "Billy Jeans",
                ISBN = "1234567890",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(newBook));

            // Assert
            Assert.Equal("Book is invalid.", exception.Message);
        }

        [Fact]
        public async Task AddBookAsync_TryToAddBookWithInvalidNullISBN_ShouldThrowException()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "New year Eve",
                Author = "Billy Jeans",
                ISBN = "",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(newBook));

            // Assert
            Assert.Equal("Book is invalid.", exception.Message);
        }

        [Fact]
        public async Task AddBookAsync_TryToAddBookWithInvalidWhiteSpaceISBN_ShouldThrowException()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "New year Eve",
                Author = "Billy Jeans",
                ISBN = " ",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(newBook));

            // Assert
            Assert.Equal("Book is invalid.", exception.Message);
        }

        [Fact]
        public async Task AddBookAsync_TryToAddBookWithInvalidLowYear_ShouldThrowException()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "Some summer thing",
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 1232,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(newBook));

            // Assert
            Assert.Equal("Book is invalid.", exception.Message);
        }

        [Fact]
        public async Task AddBookAsync_TryToAddBookWithInvalidBiggerYear_ShouldThrowException()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "Some summer thing",
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2032,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(newBook));

            // Assert
            Assert.Equal("Book is invalid.", exception.Message);
        }

        [Fact]
        public async Task AddBookAsync_TryToAddBookWithInvalidGenre_ShouldThrowException()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "Some summer thing",
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2020,
                Genre = new string('n', 55),
                Pages = 100,
                Price = 19.99
            };
            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(newBook));

            // Assert
            Assert.Equal("Book is invalid.", exception.Message);
        }


        [Fact]
        public async Task AddBookAsync_TryToAddBookWithInvalidPages_ShouldThrowException()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "Some summer thing",
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2020,
                Genre = "Action",
                Pages = -23,
                Price = 19.99
            };
            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(newBook));

            // Assert
            Assert.Equal("Book is invalid.", exception.Message);
        }

        [Fact]
        public async Task AddBookAsync_TryToAddBookWithInvalidPrice_ShouldThrowException()
        {
            // Arrange
            var newBook = new Book
            {
                Title = "Some summer thing",
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2020,
                Genre = "Action",
                Pages = 112,
                Price = -19.99
            };
            // Act
            var exception = Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(newBook));

            // Assert
            Assert.Equal("Book is invalid.", exception.Result.Message);
            var bookInDB = await _dbContext.Books.FirstOrDefaultAsync();
            Assert.Null(bookInDB);
        }


    }
}
