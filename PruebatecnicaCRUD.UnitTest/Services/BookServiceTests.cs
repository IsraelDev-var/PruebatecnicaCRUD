using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using PruebatecnicaCRUD.Core.Domain.Entities;
using PruebatecnicaCRUD.Core.Domain.Interfaces;
using PruebatecnicaCRUD.Core.Application.Services;
using PruebatecnicaCRUD.Core.Application.Dtos.Book;

namespace PruebatecnicaCRUD.UnitTest.Services
{
    public class BookServiceTests
    {
        [Fact]
        public async Task GetBooksBefore2000_ShouldReturnOnlyBooksBefore2000()
        {
            // Arrange
            var mockBookRepo = new Mock<IBookRepository>();
            var mockAuthorRepo = new Mock<IAuthorRepository>();
            var mockLogger = new Mock<ILogger<BookService>>();

            mockBookRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Book>
            {
                new Book { Id = 1, Title = "Viejo Libro", YearPublished = 1995 },
                new Book { Id = 2, Title = "Nuevo Libro", YearPublished = 2005 }
            });

            var service = new BookService(
                mockBookRepo.Object,
                mockAuthorRepo.Object,
                mockLogger.Object
            );

            // Act
            var result = await service.GetBooksBefore2000Async();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Should().HaveCount(1);
            result.Data![0].Title.Should().Be("Viejo Libro");

            mockBookRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
