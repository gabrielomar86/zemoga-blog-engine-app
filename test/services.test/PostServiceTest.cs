
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
using Xunit;

namespace BlogEngineApp.services.test
{
    public class PostServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;
        private readonly Mock<ICreationPostFlowNotifier> _mockCreationPostFlowNotifier;

        public PostServiceTest()
        {
            _mapper = new MapperConfiguration(opts => { opts.AddProfile(typeof(BlogEngineAppMappingProfile)); }).CreateMapper();
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            _mockCreationPostFlowNotifier = new Mock<ICreationPostFlowNotifier>();
        }

        [Fact]
        public void Should_CreatePost()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.Insert(It.IsAny<Post>()));

            // Act
            var resultado = posService.Create(new PostDto());

            // Assert
            Assert.IsType<PostDto>(resultado);

            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.Insert(It.IsAny<Post>()), Times.Once);
            _mockCreationPostFlowNotifier
                .Verify(notifier => notifier.PostCreated(It.IsAny<PostDto>()), Times.Once);
        }

        [Fact]
        public void Should_CreatePost_GeneralException()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.Insert(It.IsAny<Post>()))
                .Throws<Exception>();

            // Act
            Assert.Throws<Exception>(() => posService.Create(new PostDto()));

            // Assert
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.Insert(It.IsAny<Post>()), Times.Once);
            _mockCreationPostFlowNotifier
                .Verify(notifier => notifier.PostCreated(It.IsAny<PostDto>()), Times.Never);
        }

        [Fact]
        public void Should_GetById()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()))
                .Returns(new Post());

            // Act
            var resultado = posService.GetById(It.IsAny<Guid>());

            // Assert
            Assert.IsType<PostDto>(resultado);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Should_GetAll_Without_UserId()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.GetAll())
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = posService.GetAll();

            // Assert
            Assert.IsType<List<PostDto>>(resultado);
            Assert.Equal(4, resultado.Count());
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.GetAll(), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.FindByCondition(It.IsAny<Expression<Func<Post, bool>>>()), Times.Never);
        }

        [Fact]
        public void Should_GetAll_With_UserId()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.GetAll())
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = posService.GetAll("userId");

            // Assert
            Assert.IsType<List<PostDto>>(resultado);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.GetAll(), Times.Never);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.FindByCondition(It.IsAny<Expression<Func<Post, bool>>>()), Times.Once);
        }

        [Fact]
        public void Should_Reject()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()))
                .Returns(new Post());

            // Act
            var resultado = posService.Reject(It.IsAny<Guid>());

            // Assert
            Assert.IsType<PostDto>(resultado);

            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.Update(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public void Should_Reject_NotFound()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()))
                .Returns(() => null);

            // Act - Assert
            var abc = Assert.Throws<NotFoundResponseException>(() => posService.Reject(It.IsAny<Guid>()));
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.Update(It.IsAny<Post>()), Times.Never);
        }

        [Fact]
        public void Should_Pending()
        {
            // Arrange
            var postServiceMock = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()))
                .Returns(new Post());

            // Act
            var resultado = postServiceMock.Pending(It.IsAny<Guid>());

            // Assert
            Assert.IsType<PostDto>(resultado);

            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.Update(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public void Should_Pending_NotFound()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()))
                .Returns(() => null);

            // Act - Assert
            Assert.Throws<NotFoundResponseException>(() => posService.Pending(It.IsAny<Guid>()));
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.Update(It.IsAny<Post>()), Times.Never);
        }

        [Fact]
        public void Should_Approve()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()))
                .Returns(new Post());

            // Act
            var resultado = posService.Approve(It.IsAny<Guid>());

            // Assert
            Assert.IsType<PostDto>(resultado);

            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.Update(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public void Should_Approve_NotFound()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()))
                .Returns(() => null);

            // Act - Assert
            Assert.Throws<NotFoundResponseException>(() => posService.Approve(It.IsAny<Guid>()));
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.Update(It.IsAny<Post>()), Times.Never);
        }

        #region  WITHOUT_USERID

        [Fact]
        public void Should_GetAllPending_Without_UserId()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.FindByPostStatus(It.IsAny<PostStatus>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = posService.GetAllPending();

            // Assert
            Assert.IsType<List<PostPresenter>>(resultado);

            AssertWithoutUserId();
        }

        [Fact]
        public void Should_GetAllApproved_Without_UserId()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.FindByPostStatus(It.IsAny<PostStatus>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = posService.GetAllApproved();

            // Assert
            Assert.IsType<List<PostDto>>(resultado);

            AssertWithoutUserId();
        }

        [Fact]
        public void Should_GetAllRejected_Without_UserId()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.FindByPostStatus(It.IsAny<PostStatus>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = posService.GetAllRejected();

            // Assert
            Assert.IsType<List<PostDto>>(resultado);

            AssertWithoutUserId();
        }

        #endregion

        #region  WITH_USERID

        [Fact]
        public void Should_GetAllPending_With_UserId()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.FindByPostStatusAndUserId(It.IsAny<PostStatus>(),
                                                                                    It.IsAny<string>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = posService.GetAllPending("userId");

            // Assert
            Assert.IsType<List<PostPresenter>>(resultado);

            AssertWithUserId();
        }

        [Fact]
        public void Should_GetAllApproved_With_UserId()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.FindByPostStatusAndUserId(It.IsAny<PostStatus>(),
                                                                                    It.IsAny<string>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = posService.GetAllApproved("userId");

            // Assert
            Assert.IsType<List<PostDto>>(resultado);

            AssertWithUserId();
        }

        [Fact]
        public void Should_GetAllRejected_With_UserId()
        {
            // Arrange
            var posService = GetPostService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.PostRepository.FindByPostStatusAndUserId(It.IsAny<PostStatus>(),
                                                                                    It.IsAny<string>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = posService.GetAllRejected("userId");

            // Assert
            Assert.IsType<List<PostDto>>(resultado);

            AssertWithUserId();
        }

        #endregion

        #region Private Methods

        private IPostService GetPostService()
        {
            return new PostService(_mapper, _mockRepositoryWrapper.Object, _mockCreationPostFlowNotifier.Object);
        }

        private static IQueryable<Post> GetEntitiesQueryable()
        {
            var listado = new List<Post>
            {
                new Post { Id = Guid.Parse("5ee251fd-14ee-480f-b6d8-da60e1193bf1"), Status = PostStatus.Approved, UserId = "UserId1" },
                new Post { Id = Guid.Parse("ff5ee301-c3ed-4110-896d-7149d5ab1831"), Status = PostStatus.Pending, UserId = "UserId2" },
                new Post { Id = Guid.Parse("1ddc8f74-169a-4bf3-bae4-d215c735b180"), Status = PostStatus.Pending, UserId = "UserId2.1" },
                new Post { Id = Guid.Parse("e0b0c0ec-b39a-4b19-a8d5-93c7398a5407"), Status = PostStatus.Rejected, UserId = "UserId3" },
            };

            return listado.AsQueryable();
        }

        private void AssertWithUserId()
        {
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.FindByPostStatus(It.IsAny<PostStatus>()), Times.Never);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository
                                    .FindByPostStatusAndUserId(It.IsAny<PostStatus>(),
                                                                It.IsAny<string>()), Times.Once);
        }

        private void AssertWithoutUserId()
        {
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository.FindByPostStatus(It.IsAny<PostStatus>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.PostRepository
                                    .FindByPostStatusAndUserId(It.IsAny<PostStatus>(),
                                                                It.IsAny<string>()), Times.Never);
        }

        #endregion
    }
}
