using BlogEngineApp.core.dto;
using BlogEngineApp.core.interfaces;
using System;
using System.Collections.Generic;
using AutoMapper;
using BlogEngineApp.core.enums;

namespace BlogEngineApp.services
{
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public BlogService(IMapper mapper,
                            IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public BlogDto GetById(Guid id)
        {
            var entity = _repositoryWrapper
                .BlogEngineAppRepository
                .GetById(id);

            return _mapper.Map<BlogDto>(entity);
        }

        public IEnumerable<BlogDto> GetAll()
        {
            var entities = _repositoryWrapper.BlogEngineAppRepository.GetAll();
            return _mapper.Map<List<BlogDto>>(entities);
        }

        public IEnumerable<BlogDto> GetAllPending()
        {
            var entities = _repositoryWrapper.BlogEngineAppRepository.FindByBlogStatus(BlogStatus.Pending);
            return _mapper.Map<List<BlogDto>>(entities);
        }

        public IEnumerable<BlogDto> GetAllApproved()
        {
            var entities = _repositoryWrapper.BlogEngineAppRepository.FindByBlogStatus(BlogStatus.Approved);
            return _mapper.Map<List<BlogDto>>(entities);
        }

        public IEnumerable<BlogDto> GetAllRejected()
        {
            var entities = _repositoryWrapper.BlogEngineAppRepository.FindByBlogStatus(BlogStatus.Rejected);
            return _mapper.Map<List<BlogDto>>(entities);
        }

        public BlogDto Reject(Guid blogId)
        {
            return UpdateStatus(blogId, BlogStatus.Rejected);
        }

        public BlogDto Approve(Guid blogId)
        {
            return UpdateStatus(blogId, BlogStatus.Approved);
        }

        private BlogDto UpdateStatus(Guid blogId, BlogStatus status)
        {
            var blog = _repositoryWrapper.BlogEngineAppRepository.GetById(blogId);

            if (blog == null)
                throw new Exception("Blog not found");

            blog.Status = status;
            _repositoryWrapper.BlogEngineAppRepository.Update(blog);
            return _mapper.Map<BlogDto>(blog);
        }
    }
}