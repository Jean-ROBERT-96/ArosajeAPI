using ArosajeAPI.Controllers;
using DataContext;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTest.ArosajeAPI
{
    public class MessageControllerTest
    {
        private Mock<IRepository<Message>> _repoMock;
        private MessageController _controller;

        public MessageControllerTest()
        {
            _repoMock = new Mock<IRepository<Message>>();
            _controller = new MessageController(_repoMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsMessage()
        {
            // Arrange
            var expectedMessage = new Message { };
            _repoMock.Setup(repo => repo.Get(It.IsAny<Int64>())).ReturnsAsync(expectedMessage);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var message = Assert.IsType<Message>(okResult.Value);
            Assert.Equal(expectedMessage, message);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfMessages()
        {
            // Arrange
            var expectedMessages = new List<Message> { };
            _repoMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedMessages);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var messages = Assert.IsAssignableFrom<List<Message>>(okResult.Value);
            Assert.Equal(expectedMessages, messages);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var messageToCreate = new Message { };
            var createdMessage = new Message { };
            _repoMock.Setup(repo => repo.Post(It.IsAny<Message>())).ReturnsAsync(createdMessage);

            // Act
            var result = await _controller.Post(messageToCreate);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedMessage = Assert.IsType<Message>(createdAtActionResult.Value);
            Assert.Equal(createdMessage, returnedMessage);
        }

        [Fact]
        public async Task Put_ReturnsOk()
        {
            // Arrange
            var messageToUpdate = new Message { };
            _repoMock.Setup(repo => repo.Put(It.IsAny<Message>())).ReturnsAsync(messageToUpdate);

            // Act
            var result = await _controller.Put(messageToUpdate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var updatedMessage = Assert.IsType<Message>(okResult.Value);
            Assert.Equal(messageToUpdate, updatedMessage);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var messageToDelete = new Message { };
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Int64>())).ReturnsAsync(messageToDelete);

            // Act
            var result = await _controller.Delete(messageToDelete);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var deletedMessage = Assert.IsType<Message>(okResult.Value);
            Assert.Equal(messageToDelete, deletedMessage);
        }
    }
}
