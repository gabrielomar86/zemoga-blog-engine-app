
using AutoMapper;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.extensions;
using BlogEngineApp.core.interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace BlogEngineApp.services.test
{
    public class BlogServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepositoryWrapper;

        public BlogServiceTest()
        {
            _mapper = new MapperConfiguration(opts => { opts.AddProfile(typeof(BlogEngineAppMappingProfile)); }).CreateMapper();
            _mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
        }

        [Fact]
        public void Should_CreateBlog()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.Insert(It.IsAny<Blog>()));

            // Act
            var resultado = blogEngineAppService.Create(new BlogDto());

            // Assert
            Assert.IsType<BlogDto>(resultado);

            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.Insert(It.IsAny<Blog>()), Times.Once);
        }

        [Fact]
        public void Should_GetById()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.GetById(It.IsAny<Guid>()))
                .Returns(new Blog());

            // Act
            var resultado = blogEngineAppService.GetById(It.IsAny<Guid>());

            // Assert
            Assert.IsType<BlogDto>(resultado);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void Should_GetAll_Without_UserId()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.GetAll())
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = blogEngineAppService.GetAll();

            // Assert
            Assert.IsType<List<BlogDto>>(resultado);
            Assert.Equal(4, resultado.Count());
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.GetAll(), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.FindByCondition(It.IsAny<Expression<Func<Blog, bool>>>()), Times.Never);
        }

        [Fact]
        public void Should_GetAll_With_UserId()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.GetAll())
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = blogEngineAppService.GetAll("userId");

            // Assert
            Assert.IsType<List<BlogDto>>(resultado);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.GetAll(), Times.Never);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.FindByCondition(It.IsAny<Expression<Func<Blog, bool>>>()), Times.Once);
        }

        [Fact]
        public void Should_Reject()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.GetById(It.IsAny<Guid>()))
                .Returns(new Blog());

            // Act
            var resultado = blogEngineAppService.Reject(It.IsAny<Guid>());

            // Assert
            Assert.IsType<BlogDto>(resultado);

            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.Update(It.IsAny<Blog>()), Times.Once);
        }

        [Fact]
        public void Should_Reject_NotFound()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.GetById(It.IsAny<Guid>()))
                .Returns(() => null);

            // Act - Assert
            Assert.Throws<Exception>(() => blogEngineAppService.Reject(It.IsAny<Guid>()));
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.Update(It.IsAny<Blog>()), Times.Never);
        }

        [Fact]
        public void Should_Approve()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.GetById(It.IsAny<Guid>()))
                .Returns(new Blog());

            // Act
            var resultado = blogEngineAppService.Approve(It.IsAny<Guid>());

            // Assert
            Assert.IsType<BlogDto>(resultado);

            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.Update(It.IsAny<Blog>()), Times.Once);
        }

        [Fact]
        public void Should_Approve_NotFound()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.GetById(It.IsAny<Guid>()))
                .Returns(() => null);

            // Act - Assert
            Assert.Throws<Exception>(() => blogEngineAppService.Approve(It.IsAny<Guid>()));
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.GetById(It.IsAny<Guid>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.Update(It.IsAny<Blog>()), Times.Never);
        }

        #region  WITHOUT_USERID

        [Fact]
        public void Should_GetAllPending_Without_UserId()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.FindByBlogStatus(It.IsAny<BlogStatus>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = blogEngineAppService.GetAllPending();

            // Assert
            Assert.IsType<List<BlogDto>>(resultado);

            AssertWithoutUserId();
        }

        [Fact]
        public void Should_GetAllApproved_Without_UserId()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.FindByBlogStatus(It.IsAny<BlogStatus>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = blogEngineAppService.GetAllApproved();

            // Assert
            Assert.IsType<List<BlogDto>>(resultado);

            AssertWithoutUserId();
        }

        [Fact]
        public void Should_GetAllRejected_Without_UserId()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.FindByBlogStatus(It.IsAny<BlogStatus>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = blogEngineAppService.GetAllRejected();

            // Assert
            Assert.IsType<List<BlogDto>>(resultado);

            AssertWithoutUserId();
        }

        #endregion

        #region  WITH_USERID

        [Fact]
        public void Should_GetAllPending_With_UserId()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.FindByBlogStatusAndUserId(It.IsAny<BlogStatus>(),
                                                                                    It.IsAny<string>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = blogEngineAppService.GetAllPending("userId");

            // Assert
            Assert.IsType<List<BlogDto>>(resultado);

            AssertWithUserId();
        }

        [Fact]
        public void Should_GetAllApproved_With_UserId()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.FindByBlogStatusAndUserId(It.IsAny<BlogStatus>(),
                                                                                    It.IsAny<string>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = blogEngineAppService.GetAllApproved("userId");

            // Assert
            Assert.IsType<List<BlogDto>>(resultado);

            AssertWithUserId();
        }

        [Fact]
        public void Should_GetAllRejected_With_UserId()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlobRepository.FindByBlogStatusAndUserId(It.IsAny<BlogStatus>(),
                                                                                    It.IsAny<string>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = blogEngineAppService.GetAllRejected("userId");

            // Assert
            Assert.IsType<List<BlogDto>>(resultado);

            AssertWithUserId();
        }

        #endregion

        #region Private Methods

        private IBlogService GetBlogService()
        {
            return new BlogService(_mapper, _mockRepositoryWrapper.Object);
        }

        private static IQueryable<Blog> GetEntitiesQueryable()
        {
            var listado = new List<Blog>
            {
                new Blog { Id = Guid.Parse("5ee251fd-14ee-480f-b6d8-da60e1193bf1"), Status = BlogStatus.Approved, UserId = "UserId1" },
                new Blog { Id = Guid.Parse("ff5ee301-c3ed-4110-896d-7149d5ab1831"), Status = BlogStatus.Pending, UserId = "UserId2" },
                new Blog { Id = Guid.Parse("1ddc8f74-169a-4bf3-bae4-d215c735b180"), Status = BlogStatus.Pending, UserId = "UserId2.1" },
                new Blog { Id = Guid.Parse("e0b0c0ec-b39a-4b19-a8d5-93c7398a5407"), Status = BlogStatus.Rejected, UserId = "UserId3" },
            };

            return listado.AsQueryable();
        }

        private void AssertWithUserId()
        {
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.FindByBlogStatus(It.IsAny<BlogStatus>()), Times.Never);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository
                                    .FindByBlogStatusAndUserId(It.IsAny<BlogStatus>(),
                                                                It.IsAny<string>()), Times.Once);
        }

        private void AssertWithoutUserId()
        {
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository.FindByBlogStatus(It.IsAny<BlogStatus>()), Times.Once);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlobRepository
                                    .FindByBlogStatusAndUserId(It.IsAny<BlogStatus>(),
                                                                It.IsAny<string>()), Times.Never);
        }

        #endregion
    }
}
