using LibraryManagementSystem;

namespace LibraryManagementTest
{
        [TestFixture]
        public class LibraryTests
        {
            
            [Test]
            public void AddBook_SholdAddNewBookToTheLibrary()
            {
                //Arrange
                Library library = new Library();
            var newBook = new Book{ 
            Author = "John Smith",
            Id = 1,
            IsCheckedOut = false,
            Title = "Title",
            };
                //Act
                library.AddBook(newBook);

            //Assert
            var allBooks = library.GetAllBooks();
            Assert.That(allBooks.Count(), Is.EqualTo(1));

            var singleBook = allBooks.First();
            Assert.That(singleBook.Id, Is.EqualTo(1));
            Assert.That(singleBook.Author, Is.EqualTo(newBook.Author));
            Assert.That(singleBook.Title, Is.EqualTo("Title"));
            Assert.IsFalse(singleBook.IsCheckedOut);

            }

        [Test]
        public void CheckOutBook_ShouldReturnFalse_IfBookDoesNotExist()
        {
            //Arrange
            Library library = new Library();
            var newBook = new Book
            {
                Author = "John Smith",
                Id = 1,
                IsCheckedOut = false,
                Title = "Title",
            };
            library.AddBook(newBook);
            //Act
            var result = library.CheckOutBook(99);

            //Assert
            Assert.IsFalse(result);

        }

        [Test]
        public void CheckOutBook_ShouldReturnFalse_IfBookAlreadyCheckOut()
        {
            //Arrange
            Library library = new Library();
            var newBook = new Book
            {
                Author = "John Smith",
                Id = 1,
                IsCheckedOut = true,
                Title = "Title",
            };
            library.AddBook(newBook);
            //Act
            
            var result = library.CheckOutBook(newBook.Id);

            //Assert
            Assert.IsFalse(result);
        }


        [Test]
        public void CheckOutBook_ShouldReturnTrue_IfWeCanCheckOutTheBook()
        {
            //Arrange
            Library library = new Library();
            var newBook = new Book
            {
                Author = "John Smith",
                Id = 1,
                IsCheckedOut = false,
                Title = "Title",
            };
            library.AddBook(newBook);
            //Act

            var result = library.CheckOutBook(newBook.Id);

            //Assert
            Assert.IsTrue(result);
            var allBooks = library.GetAllBooks();
            var singleBook = allBooks.First();
            Assert.IsTrue(singleBook.IsCheckedOut);
        }

        [Test]
        public void ReturnBook_ShouldReturnFalse_IfBookDoesNotExist()
        {
            //Arrange
            Library library = new Library();
            var newBook = new Book
            {
                Author = "John Smith",
                Id = 1,
                IsCheckedOut = false,
                Title = "Title",
            };
            library.AddBook(newBook);
            //Act
            var result = library.ReturnBook(99);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ReturnBook_ShouldReturnFalse_IfBookIsNotCheckedOut()
        {
            //Arrange
            Library library = new Library();
            var newBook = new Book
            {
                Author = "John Smith",
                Id = 1,
                IsCheckedOut = false,
                Title = "Title",
            };
            library.AddBook(newBook);
            //Act
            var result = library.ReturnBook(1);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ReturnBook_ShouldReturnTrue_IfBookCanBeCkeckedOut()
        {
            //Arrange
            Library library = new Library();
            var newBook = new Book
            {
                Author = "John Smith",
                Id = 1,
                IsCheckedOut = true,
                Title = "Title",
            };
            library.AddBook(newBook);
            //Act
            var result = library.ReturnBook(1);

            //Assert
            Assert.IsTrue(result);
            var allBooks = library.GetAllBooks();
            var singleBook = allBooks.First();
            Assert.IsFalse(singleBook.IsCheckedOut);
        }



    }
    }
