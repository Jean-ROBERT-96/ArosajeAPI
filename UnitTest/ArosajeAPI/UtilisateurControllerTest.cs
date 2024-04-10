using ArosajeAPI.Controllers;
using DataContext;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTest.ArosajeAPI
{
    public class UtilisateurControllerTest
    {
        private Mock<IRepository<Utilisateur>> _repoMock;
        private UtilisateurController _controller;

        public UtilisateurControllerTest()
        {
            _repoMock = new Mock<IRepository<Utilisateur>>();
            _controller = new UtilisateurController(_repoMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsUtilisateur()
        {
            // Arrange
            var expectedUtilisateur = new Utilisateur {  };
            _repoMock.Setup(repo => repo.Get(It.IsAny<Int64>())).ReturnsAsync(expectedUtilisateur);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var utilisateur = Assert.IsType<Utilisateur>(okResult.Value);
            Assert.Equal(expectedUtilisateur, utilisateur);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfUtilisateurs()
        {
            // Arrange
            var expectedUtilisateurs = new List<Utilisateur> {  };
            _repoMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedUtilisateurs);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var utilisateurs = Assert.IsAssignableFrom<List<Utilisateur>>(okResult.Value);
            Assert.Equal(expectedUtilisateurs, utilisateurs);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var utilisateurToCreate = new Utilisateur {  };
            var createdUtilisateur = new Utilisateur {  };
            _repoMock.Setup(repo => repo.Post(It.IsAny<Utilisateur>())).ReturnsAsync(createdUtilisateur);

            // Act
            var result = await _controller.Post(utilisateurToCreate);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedUtilisateur = Assert.IsType<Utilisateur>(createdAtActionResult.Value);
            Assert.Equal(createdUtilisateur, returnedUtilisateur);
        }

        [Fact]
        public async Task Put_ReturnsOk()
        {
            // Arrange
            var utilisateurToUpdate = new Utilisateur {  };
            _repoMock.Setup(repo => repo.Put(It.IsAny<Utilisateur>())).ReturnsAsync(utilisateurToUpdate);

            // Act
            var result = await _controller.Put(utilisateurToUpdate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var updatedUtilisateur = Assert.IsType<Utilisateur>(okResult.Value);
            Assert.Equal(utilisateurToUpdate, updatedUtilisateur);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var utilisateurToDelete = new Utilisateur {  };
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Int64>())).ReturnsAsync(utilisateurToDelete);

            // Act
            var result = await _controller.Delete(utilisateurToDelete);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var deletedUtilisateur = Assert.IsType<Utilisateur>(okResult.Value);
            Assert.Equal(utilisateurToDelete, deletedUtilisateur);
        }
    }
}
