using MongoDB.Driver;
using MoviesLibraryAPI.Controllers;
using MoviesLibraryAPI.Controllers.Contracts;
using MoviesLibraryAPI.Data.Models;
using MoviesLibraryAPI.Data.Seed;
using MoviesLibraryAPI.Services;
using MoviesLibraryAPI.Services.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MoviesLibraryAPI.XUnitTests
{
    public class XUnitIntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly MoviesLibraryXUnitTestDbContext _dbContext;
        private readonly IMoviesLibraryController _controller;
        private readonly IMoviesRepository _repository;

        public XUnitIntegrationTests(DatabaseFixture fixture)
        {
            _dbContext = fixture.DbContext;
            _repository = new MoviesRepository(_dbContext.Movies);
            _controller = new MoviesLibraryController(_repository);

            InitializeDatabaseAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeDatabaseAsync()
        {
            await _dbContext.ClearDatabaseAsync();
        }

        [Fact]
        public async Task AddMovieAsync_WhenValidMovieProvided_ShouldAddToDatabase()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };

            // Act
            await _controller.AddAsync(movie);

            // Assert
            var resultMovie = await _dbContext.Movies.Find(m => m.Title == "Test Movie").FirstOrDefaultAsync();
            Xunit.Assert.NotNull(resultMovie);
            Xunit.Assert.Equal("Test Movie", resultMovie.Title);
            Xunit.Assert.Equal("Test Director", resultMovie.Director);
            Xunit.Assert.Equal(2022, resultMovie.YearReleased);
            Xunit.Assert.Equal("Action", resultMovie.Genre);
            Xunit.Assert.Equal(120, resultMovie.Duration);
            Xunit.Assert.Equal(7.5, resultMovie.Rating);
        }

        [Fact]
        public async Task AddMovieAsync_WhenInvalidMovieProvided_ShouldThrowValidationException()
        {
            // Arrange
            var invalidMovie = new Movie
            {
                Title = new string('A', 200),
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };

            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _controller.AddAsync(invalidMovie));

            // Assert
            Assert.Equal("Movie is not valid.", exception.Message);

        }

        [Fact]
        public async Task AddMovieAsync_TryToAddMovieWithInvalidRating_ShouldThrowValidationException()
        {

            //Arrange
            var invalidMovieInvalidRating = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 12.5
            };
            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _controller.AddAsync(invalidMovieInvalidRating));

            // Assert
            Assert.Equal("Movie is not valid.", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenValidTitleProvided_ShouldDeleteMovie()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            var movieFan = new Movie
            {
                Title = "Test Movie Test",
                Director = "Test Director Two",
                YearReleased = 2023,
                Genre = "Fantasy",
                Duration = 100,
                Rating = 8.5
            };
            await _controller.AddAsync(movie);
            await _controller.AddAsync(movieFan);

            var movieToDelete = await _controller.GetByTitle("Test Movie");
            // Act            
            await _controller.DeleteAsync(movieToDelete.Title);
            // Assert
            var result = await _controller.GetAllAsync();
            Assert.Equal(1, result.Count());
           
           
        }


        [Fact]
        public async Task DeleteAsync_WhenTitleIsNull_ShouldThrowArgumentException()
        {
            // Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _controller.DeleteAsync(null));

            // Assert
            Assert.Equal("Title cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenTitleIsEmpty_ShouldThrowArgumentException()
        {
            

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _controller.DeleteAsync(""));

            // Assert
            Assert.Equal("Title cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenTitleDoesNotExist_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            _controller.AddAsync(movie);

            // Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _controller.DeleteAsync("Star movie"));

            // Assert
            Assert.Equal("Movie with title 'Star movie' not found.", exception.Message);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoMoviesExist_ShouldReturnEmptyList()
        {
            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            Assert.Empty(result);
            

        }

        [Fact]
        public async Task GetAllAsync_WhenMoviesExist_ShouldReturnAllMovies()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };

            var movieNo = new Movie
            {
                Title = "Test Movie No",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 8.5
            };

            var movieYes = new Movie
            {
                Title = "Test Movie Yes",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 9.5
            };

            await _controller.AddAsync(movie);
            await _controller.AddAsync(movieNo);
            await _controller.AddAsync(movieYes);


            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            Assert.Equal(3, result.Count());
            Assert.True(result.Any());
            Assert.Equal("Test Movie Yes", movieYes.Title);
        }

        [Fact]
        public async Task GetByTitle_WhenTitleExists_ShouldReturnMatchingMovie()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            await _controller.AddAsync(movie);

            // Act

            var resultMovie = await _controller.GetByTitle("Test Movie");
            // Assert
            Assert.Equal(movie.Title, resultMovie.Title);
        }

        [Fact]
        public async Task GetByTitle_WhenTitleDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            await _controller.AddAsync(movie);

            // Act

            var resultMovie = await _controller.GetByTitle("Star Movie");
            // Assert
            Assert.Null(resultMovie);
        }


        [Fact]
        public async Task SearchByTitleFragmentAsync_WhenTitleFragmentExists_ShouldReturnMatchingMovies()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            await _controller.AddAsync(movie);

            // Act

            var resultMovie = await _controller.GetByTitle("Test Movie");
            // Assert
            Assert.Equal("Test Movie", resultMovie.Title);
        }

        [Fact]
        public async Task SearchByTitleFragmentAsync_WhenNoMatchingTitleFragment_ShouldThrowKeyNotFoundException()
        {
           
            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() 
                => _controller.SearchByTitleFragmentAsync("star wars"));

            // Assert
            Assert.Equal("No movies found.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_WhenValidMovieProvided_ShouldUpdateMovie()
        {
            // Arrange
            var movie = new Movie
            {
                Title = "Test Movie",
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };
            await _controller.AddAsync(movie);

            // Act
            movie.Title = "Update test Movie";
            _controller.UpdateAsync(movie);

            // Assert
            Assert.Equal("Update test Movie", movie.Title);
        }

        [Fact]
        public async Task UpdateAsync_WhenInvalidMovieProvided_ShouldThrowValidationException()
        {
            // Arrange
            var invalidMovie = new Movie
            {
               
                Director = "Test Director",
                YearReleased = 2022,
                Genre = "Action",
                Duration = 120,
                Rating = 7.5
            };

            // Act
            invalidMovie.Rating = 1;
         
            var exception = await Assert.ThrowsAsync<ValidationException>(() 
                => _controller.UpdateAsync(invalidMovie));

            // Assert
            Assert.Equal("Movie is not valid.", exception.Message);
        }
    }
}
