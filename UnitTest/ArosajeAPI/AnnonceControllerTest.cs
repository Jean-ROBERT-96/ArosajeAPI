using ArosajeAPI.Controllers;
using DataContext;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTest.ArosajeAPI
{
    public class AnnonceControllerTest
    {
        private Mock<IRepository<Annonce>> _repoMock;
        private AnnonceController _controller;

        public AnnonceControllerTest()
        {
            _repoMock = new Mock<IRepository<Annonce>>();
            _controller = new AnnonceController(_repoMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsAnnonce()
        {
            // Arrange
            var expectedAnnonce = new Annonce { };
            _repoMock.Setup(repo => repo.Get(It.IsAny<Int64>())).ReturnsAsync(expectedAnnonce);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var annonce = Assert.IsType<Annonce>(okResult.Value);
            Assert.Equal(expectedAnnonce, annonce);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfAnnonces()
        {
            // Arrange
            var expectedAnnonces = new List<Annonce> { };
            _repoMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedAnnonces);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var annonces = Assert.IsAssignableFrom<List<Annonce>>(okResult.Value);
            Assert.Equal(expectedAnnonces, annonces);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var annonceToCreate = new Annonce { };
            var createdAnnonce = new Annonce { };
            _repoMock.Setup(repo => repo.Post(It.IsAny<Annonce>())).ReturnsAsync(createdAnnonce);

            // Act
            var result = await _controller.Post(annonceToCreate);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedAnnonce = Assert.IsType<Annonce>(createdAtActionResult.Value);
            Assert.Equal(createdAnnonce, returnedAnnonce);
        }

        [Fact]
        public async Task Put_ReturnsOk()
        {
            // Arrange
            var annonceToUpdate = new Annonce { };
            _repoMock.Setup(repo => repo.Put(It.IsAny<Annonce>())).ReturnsAsync(annonceToUpdate);

            // Act
            var result = await _controller.Put(annonceToUpdate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var updatedAnnonce = Assert.IsType<Annonce>(okResult.Value);
            Assert.Equal(annonceToUpdate, updatedAnnonce);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var annonceToDelete = new Annonce { };
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Int64>())).ReturnsAsync(annonceToDelete);

            // Act
            var result = await _controller.Delete(annonceToDelete);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var deletedAnnonce = Assert.IsType<Annonce>(okResult.Value);
            Assert.Equal(annonceToDelete, deletedAnnonce);
        }
    }
}
