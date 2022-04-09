
using AutoMapper;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.extensiones;
using BlogEngineApp.core.interfaces.contratos;
using BlogEngineApp.core.interfaces.contratos.servicios;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace BlogEngineApp.services.test
{
    public class BlogEngineAppServicioTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepositorioWrapper;

        public BlogEngineAppServicioTest()
        {
            _mapper = new MapperConfiguration(opts => { opts.AddProfile(typeof(BlogEngineAppMappingProfile)); }).CreateMapper();
            _mockRepositorioWrapper = new Mock<IRepositoryWrapper>();
        }

        [Fact]
        public void Should_ObtenerPorId()
        {
            // Arrange
            var BlogEngineAppServicio = ObtenerBlogEngineAppServicio();
            _mockRepositorioWrapper
                .Setup(mrw => mrw.BlogEngineAppRepository.FindByCondition(It.IsAny<Expression<Func<Blog, bool>>>()))
                .Returns(ObtenerEntidadesQueryable());

            // Act
            var resultado = BlogEngineAppServicio.GetById(It.IsAny<Guid>());

            // Assert
            Assert.IsType<BlogDto>(resultado);
            _mockRepositorioWrapper
                .Verify(mrw => mrw.BlogEngineAppRepository.FindByCondition(It.IsAny<Expression<Func<Blog, bool>>>()), Times.Once);
        }

        [Fact]
        public void Should_ObtenerTodos()
        {
            // Arrange
            var BlogEngineAppServicio = ObtenerBlogEngineAppServicio();
            _mockRepositorioWrapper
                .Setup(mrw => mrw.BlogEngineAppRepository.GetAll())
                .Returns(ObtenerEntidadesQueryable());

            // Act
            var resultado = BlogEngineAppServicio.GetAll();

            // Assert
            Assert.IsType<List<BlogDto>>(resultado);
            Assert.Single(resultado);
            _mockRepositorioWrapper
                .Verify(mrw => mrw.BlogEngineAppRepository.GetAll(), Times.Once);
        }

        #region Private Methods

        private IBlogService ObtenerBlogEngineAppServicio()
        {
            return new BlogServicio(_mapper, _mockRepositorioWrapper.Object);
        }

        private IQueryable<Blog> ObtenerEntidadesQueryable()
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
