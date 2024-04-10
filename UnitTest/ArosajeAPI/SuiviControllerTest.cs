using ArosajeAPI.Controllers;
using DataContext;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTest.ArosajeAPI
{
    public class SuiviControllerTest
    {
        private Mock<IRepository<Suivi>> _repoMock;
        private SuiviController _controller;

        public SuiviControllerTest()
        {
            _repoMock = new Mock<IRepository<Suivi>>();
            _controller = new SuiviController(_repoMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsSuivi()
        {
            // Arrange
            var expectedSuivi = new Suivi { };
            _repoMock.Setup(repo => repo.Get(It.IsAny<Int64>())).ReturnsAsync(expectedSuivi);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var suivi = Assert.IsType<Suivi>(okResult.Value);
            Assert.Equal(expectedSuivi, suivi);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfSuivis()
        {
            // Arrange
            var expectedSuivis = new List<Suivi> { };
            _repoMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedSuivis);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var suivis = Assert.IsAssignableFrom<List<Suivi>>(okResult.Value);
            Assert.Equal(expectedSuivis, suivis);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var suiviToCreate = new Suivi { };
            var createdSuivi = new Suivi { };
            _repoMock.Setup(repo => repo.Post(It.IsAny<Suivi>())).ReturnsAsync(createdSuivi);

            // Act
            var result = await _controller.Post(suiviToCreate);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedSuivi = Assert.IsType<Suivi>(createdAtActionResult.Value);
            Assert.Equal(createdSuivi, returnedSuivi);
        }

        [Fact]
        public async Task Put_ReturnsOk()
        {
            // Arrange
            var suiviToUpdate = new Suivi { };
            _repoMock.Setup(repo => repo.Put(It.IsAny<Suivi>())).ReturnsAsync(suiviToUpdate);

            // Act
            var result = await _controller.Put(suiviToUpdate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var updatedSuivi = Assert.IsType<Suivi>(okResult.Value);
            Assert.Equal(suiviToUpdate, updatedSuivi);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var suiviToDelete = new Suivi { };
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Int64>())).ReturnsAsync(suiviToDelete);

            // Act
            var result = await _controller.Delete(suiviToDelete);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var deletedSuivi = Assert.IsType<Suivi>(okResult.Value);
            Assert.Equal(suiviToDelete, deletedSuivi);
        }
    }
}
