
using AutoMapper;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.entities;
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
        public void Should_GetById()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlogEngineAppRepository.FindByCondition(It.IsAny<Expression<Func<Blog, bool>>>()))
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = blogEngineAppService.GetById(It.IsAny<Guid>());

            // Assert
            Assert.IsType<BlogDto>(resultado);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlogEngineAppRepository.FindByCondition(It.IsAny<Expression<Func<Blog, bool>>>()), Times.Once);
        }

        [Fact]
        public void Should_GetAll()
        {
            // Arrange
            var blogEngineAppService = GetBlogService();
            _mockRepositoryWrapper
                .Setup(mrw => mrw.BlogEngineAppRepository.GetAll())
                .Returns(GetEntitiesQueryable());

            // Act
            var resultado = blogEngineAppService.GetAll();

            // Assert
            Assert.IsType<List<BlogDto>>(resultado);
            Assert.Single(resultado);
            _mockRepositoryWrapper
                .Verify(mrw => mrw.BlogEngineAppRepository.GetAll(), Times.Once);
        }

        #region Private Methods

        private IBlogService GetBlogService()
        {
            return new BlogService(_mapper, _mockRepositoryWrapper.Object);
        }

        private static IQueryable<Blog> GetEntitiesQueryable()
        {
            var listado = new List<Blog>
            {
                new Blog()
            };

            return listado.AsQueryable();
        }
        #endregion
    }
}
