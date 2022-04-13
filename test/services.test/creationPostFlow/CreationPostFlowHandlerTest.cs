
using AutoMapper;
using BlogEngineApp.core;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.extensions;
using BlogEngineApp.core.interfaces;
using BlogEngineApp.core.presenter;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace BlogEngineApp.services.test
{
    public class CreationPostFlowHandlerTest
    {
        private readonly Mock<IPostService> _mockPostService;

        public CreationPostFlowHandlerTest()
        {
            _mockPostService = new Mock<IPostService>();
        }

        [Fact]
        public void Call_CreatePost_When_Send_CreatePost_Status()
        {
            // Arrange
            var creationPostFlowHandler = GetCreationPostFlowHandler();
            var creationPostFlowCommand = GetCreationPostFlowCommand(PostStatus.CreatePost);

            // Act
            var result = creationPostFlowHandler.Handle(creationPostFlowCommand, default);

            // Assert
            Assert.Equal(result, Task.CompletedTask);
            _mockPostService.Verify(mhs => mhs.CreatePost(It.IsAny<PostDto>()), Times.Once);
        }

        [Fact]
        public void Call_ChangePostToCreatedStatus_When_Send_UpdatePostToCreatedStatus_Status()
        {
            // Arrange
            var creationPostFlowHandler = GetCreationPostFlowHandler();
            var creationPostFlowCommand = GetCreationPostFlowCommand(PostStatus.UpdatePostToCreatedStatus);

            // Act
            var result = creationPostFlowHandler.Handle(creationPostFlowCommand, default);

            // Assert
            Assert.Equal(result, Task.CompletedTask);
            _mockPostService.Verify(mhs => mhs.ChangePostToCreatedStatus(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Call_ChangePostToPendingStatus_When_Send_PostCreated_Status()
        {
            // Arrange
            var creationPostFlowHandler = GetCreationPostFlowHandler();
            var creationPostFlowCommand = GetCreationPostFlowCommand(PostStatus.PostCreated);

            // Act
            var result = creationPostFlowHandler.Handle(creationPostFlowCommand, default);

            // Assert
            Assert.Equal(result, Task.CompletedTask);
            _mockPostService.Verify(mhs => mhs.ChangePostToPendingStatus(It.IsAny<Guid>()), Times.Once);
        }

        #region Private Methods

        private static CreationPostFlowCommand GetCreationPostFlowCommand(PostStatus status)
        {
            return new CreationPostFlowCommand
            {
                Status = status,
                Payload = new PostDto()
            };
        }

        private CreationPostFlowHandler GetCreationPostFlowHandler()
        {
            return new CreationPostFlowHandler(_mockPostService.Object);
        }

        #endregion
    }
}
