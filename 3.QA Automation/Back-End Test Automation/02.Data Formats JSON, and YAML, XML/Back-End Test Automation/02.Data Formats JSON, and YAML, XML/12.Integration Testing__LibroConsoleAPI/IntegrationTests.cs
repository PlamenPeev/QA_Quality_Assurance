using LibroConsoleAPI.Business;
using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Data.Models;
using LibroConsoleAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibroConsoleAPI.IntegrationTests
{
    public class IntegrationTests : IClassFixture<BookManagerFixture>
    {
        private readonly BookManager _bookManager;
        private readonly TestLibroDbContext _dbContext;

        public IntegrationTests(BookManagerFixture fixture)
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
                Price = - 19.99
            };
            // Act
            var exception = Assert.ThrowsAsync<ValidationException>(() => _bookManager.AddAsync(newBook));

            // Assert
            Assert.Equal("Book is invalid.", exception.Result.Message);
            var bookInDB = await _dbContext.Books.FirstOrDefaultAsync();
            Assert.Null(bookInDB);
        }

     

        [Fact]
        public async Task GetAllAsync_WhenBooksExist_ShouldReturnAllBooks()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);
            // Act
            var result =  _bookManager.GetAllAsync();
            // Assert
            Assert.Equal(10, result.Result.Count());
        }

        [Fact]
        public async Task GetAllAsync_WhenNoBooksExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act
            var exception = Assert.ThrowsAsync<KeyNotFoundException> (() => _bookManager.GetAllAsync());
            // Assert
            Assert.Equal("No books found.", exception.Result.Message);
        }

        [Fact]
        public async Task SearchByTitleAsync_WithValidTitleFragment_ShouldReturnMatchingBooks()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);
            // Act
            var result = await _bookManager.SearchByTitleAsync("The Catcher in the Rye");
            // Assert
            Assert.True(result.Any());
            Assert.Equal("The Catcher in the Rye", result.FirstOrDefault().Title);
        }

        [Fact]
        public async Task SearchByTitleAsync_WithInvalidTitleFragment_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);
            // Act
            var exception = Assert.ThrowsAsync<KeyNotFoundException> (() => _bookManager.SearchByTitleAsync("test by"));
            // Assert
            Assert.Equal("No books found with the given title fragment.", exception.Result.Message);
        }

        [Fact]
        public async Task SearchByTitleAsync_WithNullTitleFragment_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);
            // Act
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _bookManager.SearchByTitleAsync(""));
            // Assert
            Assert.Equal("Title fragment cannot be empty.", exception.Result.Message);
        }

        [Fact]
        public async Task SearchByTitleAsync_WithWhiteSpaceTitleFragment_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);
            // Act
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _bookManager.SearchByTitleAsync(" "));
            // Assert
            Assert.Equal("Title fragment cannot be empty.", exception.Result.Message);
        }



        [Fact]
        public async Task GetSpecificAsync_WithValidIsbn_ShouldReturnBook()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);
            // Act
            var result = _bookManager.GetSpecificAsync("9780765316323");
            // Assert
            Assert.Equal("The Name of the Wind", result.Result.Title);
            
        }

        [Fact]
        public async Task GetSpecificAsync_WithInvalidIsbn_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);
            // Act
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => _bookManager.GetSpecificAsync("1234567890123"));
            // Assert
            Assert.Equal("No book found with ISBN: 1234567890123", exception.Result.Message);
            var bookInDb = _dbContext.Books.ToList();
            Assert.Equal(10, bookInDb.Count);
        }

        [Fact]
        public async Task GetSpecificAsync_WithNullIsbn_ShouldThrowArgumentException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);
            // Act
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _bookManager.GetSpecificAsync(""));
            // Assert
            Assert.Equal("ISBN cannot be empty.", exception.Result.Message);
            
        }

        [Fact]
        public async Task GetSpecificAsync_WithWhiteSpaceIsbn_ShouldThrowArgumentException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);
            // Act
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _bookManager.GetSpecificAsync(" "));
            // Assert
            Assert.Equal("ISBN cannot be empty.", exception.Result.Message);
            
        }



        [Fact]
        public async Task UpdateAsync_WithValidBook_ShouldUpdateBook()
        {
            // Arrange
            //await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);
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

            newBook.Title = "Updated Title";
            _bookManager.UpdateAsync(newBook);

            //Assert
            var bookInDb = await _dbContext.Books.FirstOrDefaultAsync();
            Assert.Equal("Updated Title", bookInDb.Title);

        }

        [Fact]
        public async Task UpdateAsync_WithInvalidBook_ShouldThrowValidationException()
        {
            // Arrange
            await DatabaseSeeder.SeedDatabaseAsync(_dbContext, _bookManager);

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

            // Assert
            var exeption = Assert.ThrowsAsync<ValidationException>(() => _bookManager.UpdateAsync(newBook));
            Assert.Equal("Book is invalid.", exeption.Result.Message);
        }

    }
}
