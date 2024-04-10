using ArosajeAPI.Controllers;
using DataContext;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTest.ArosajeAPI
{
    public class ConversationControllerTest
    {
        private Mock<IRepository<Conversation>> _repoMock;
        private ConversationController _controller;

        public ConversationControllerTest()
        {
            _repoMock = new Mock<IRepository<Conversation>>();
            _controller = new ConversationController(_repoMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsConversation()
        {
            // Arrange
            var expectedConversation = new Conversation { };
            _repoMock.Setup(repo => repo.Get(It.IsAny<Int64>())).ReturnsAsync(expectedConversation);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var conversation = Assert.IsType<Conversation>(okResult.Value);
            Assert.Equal(expectedConversation, conversation);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfConversations()
        {
            // Arrange
            var expectedConversations = new List<Conversation> { };
            _repoMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedConversations);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var conversations = Assert.IsAssignableFrom<List<Conversation>>(okResult.Value);
            Assert.Equal(expectedConversations, conversations);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var conversationToCreate = new Conversation { };
            var createdConversation = new Conversation { };
            _repoMock.Setup(repo => repo.Post(It.IsAny<Conversation>())).ReturnsAsync(createdConversation);

            // Act
            var result = await _controller.Post(conversationToCreate);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedConversation = Assert.IsType<Conversation>(createdAtActionResult.Value);
            Assert.Equal(createdConversation, returnedConversation);
        }

        [Fact]
        public async Task Put_ReturnsOk()
        {
            // Arrange
            var conversationToUpdate = new Conversation { };
            _repoMock.Setup(repo => repo.Put(It.IsAny<Conversation>())).ReturnsAsync(conversationToUpdate);

            // Act
            var result = await _controller.Put(conversationToUpdate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var updatedConversation = Assert.IsType<Conversation>(okResult.Value);
            Assert.Equal(conversationToUpdate, updatedConversation);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            // Arrange
            var conversationToDelete = new Conversation { };
            _repoMock.Setup(repo => repo.Delete(It.IsAny<Int64>())).ReturnsAsync(conversationToDelete);

            // Act
            var result = await _controller.Delete(conversationToDelete);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var deletedConversation = Assert.IsType<Conversation>(okResult.Value);
            Assert.Equal(conversationToDelete, deletedConversation);
        }
    }
}
