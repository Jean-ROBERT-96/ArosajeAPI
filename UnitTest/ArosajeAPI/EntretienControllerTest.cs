using ArosajeAPI.Controllers;
using DataContext;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTest.ArosajeAPI
{
    public class EntretienControllerTest
    {
        private Mock<IRepository<Entretien>> _repoMock;
        private EntretienController _controller;

        public EntretienControllerTest()
        {
            _repoMock = new Mock<IRepository<Entretien>>();
            _controller = new EntretienController(_repoMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsEntretien()
        {
            // Arrange
            var expectedEntretien = new Entretien { };
            _repoMock.Setup(repo => repo.Get(It.IsAny<Int64>())).ReturnsAsync(expectedEntretien);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var entretien = Assert.IsType<Entretien>(okResult.Value);
            Assert.Equal(expectedEntretien, entretien);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfEntretiens()
        {
            // Arrange
            var expectedEntretiens = new List<Entretien> { };
            _repoMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedEntretiens);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var entretiens = Assert.IsAssignableFrom<List<Entretien>>(okResult.Value);
            Assert.Equal(expectedEntretiens, entretiens);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var entretienToCreate = new Entretien { };
            var createdEntretien = new Entretien { };
            _repoMock.Setup(repo => repo.Post(It.IsAny<Entretien>())).ReturnsAsync(createdEntretien);

            // Act
            var result = await _controller.Post(entretienToCreate);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedEntretien = Assert.IsType<Entretien>(createdAtActionResult.Value);
            Assert.Equal(createdEntretien, returnedEntretien);
        }

        [Fact]
        public async Task Put_ReturnsOk()
        {
            // Arrange
            var entretienToUpdate = new Entretien { };
            _repoMock.Setup(repo => repo.Put(It.IsAny<Entretien>())).ReturnsAsync(entretienToUpdate);

            // Act
            var result = await _controller.Put(entretienToUpdate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var updatedEntretien = Assert.IsType<Entretien>(okResult.Value);
            Assert.Equal(entretienToUpdate, updatedEntretien);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var entretienToDelete = new Entretien { };
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Int64>())).ReturnsAsync(entretienToDelete);

            // Act
            var result = await _controller.Delete(entretienToDelete);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var entretienSuivi = Assert.IsType<Entretien>(okResult.Value);
            Assert.Equal(entretienToDelete, entretienSuivi);
        }
    }
}
