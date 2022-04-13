
using AutoMapper;
using BlogEngineApp.core;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.interfaces;
using BlogEngineApp.core.presenter;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BlogEngineApp.services.test
{
    public class CommentServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;

        public CommentServiceTest()
        {
            _mapper = new MapperConfiguration(opts => { opts.AddProfile(typeof(BlogEngineAppMappingProfile)); }).CreateMapper();
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
        }

        [Fact]
        public void Should_GetAllCommentsByPostId()
        {
            // Arrange
            var posService = GetCommentService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.CommentRepository.FindCommentByPostId(It.IsAny<Guid>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = posService.GetAllCommentsByPostId(It.IsAny<Guid>());

            // Assert
            Assert.IsType<List<CommentPresenter>>(resultado);
            Assert.Equal(4, resultado.Count());
            _mockRepositoryWrapper
                .Verify(mrw => mrw.CommentRepository.FindCommentByPostId(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Should_CreateComment()
        {
            // Arrange
            var posService = GetCommentService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()))
                .Returns(new Post { Status = PostStatus.PostApproved });
            _mockRepositoryWrapper
                .Setup(mrw => mrw.CommentRepository.Insert(It.IsAny<Comment>()));

            // Act
            var resultado = posService.CreateComment(new CommentDto { PostId = It.IsAny<Guid>() });

            // Assert
            Assert.IsType<CommentPresenter>(resultado);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.CommentRepository.Insert(It.IsAny<Comment>()), Times.Once);
        }

        [Fact]
        public void Should_Not_CreateComment_Post_NotFound()
        {
            // Arrange
            var posService = GetCommentService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()));

            // Act / Assert
            Assert.Throws<NotFoundResponseException>(() => posService.CreateComment(new CommentDto { PostId = It.IsAny<Guid>() }));

            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.CommentRepository.Insert(It.IsAny<Comment>()), Times.Never);
        }

        [Fact]
        public void Should_Not_CreateComment_Post_Is_Not_Approved()
        {
            // Arrange
            var posService = GetCommentService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()))
                .Returns(new Post { Status = PostStatus.PostPending });

            // Act / Assert
            Assert.Throws<Exception>(() => posService.CreateComment(new CommentDto { PostId = It.IsAny<Guid>() }));

            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.CommentRepository.Insert(It.IsAny<Comment>()), Times.Never);
        }

        #region Private Methods

        private ICommentService GetCommentService()
        {
            return new CommentService(_mapper, _mockRepositoryWrapper.Object);
        }

        private static IQueryable<Comment> GetEntitiesQueryable()
        {
            var listado = new List<Comment>
            {
                new Comment { Id = Guid.Parse("5ee251fd-14ee-480f-b6d8-da60e1193bf1"), Detail = "comment1" },
                new Comment { Id = Guid.Parse("d33d6d92-29a6-4048-af43-1cf11aea3293"), Detail = "comment2" },
                new Comment { Id = Guid.Parse("390e16e8-36db-4cce-b76c-075bf0883c81"), Detail = "comment3" },
                new Comment { Id = Guid.Parse("3fec6df1-7d88-4ca9-b80d-42a6cd383b41"), Detail = "comment4" },
            };

            return listado.AsQueryable();
        }

        #endregion
    }
}
