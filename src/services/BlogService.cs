using BlogEngineApp.core.dto;
using BlogEngineApp.core.interfaces;
using System;
using System.Collections.Generic;
using AutoMapper;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.entities;

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

        public BlogDto Create(BlogDto blogDto)
        {
            var blog = _mapper.Map<Blog>(blogDto);
            _repositoryWrapper.BlobRepository.Insert(blog);
            return _mapper.Map<BlogDto>(blog);
        }

        public BlogDto GetById(Guid id)
        {
            var entity = _repositoryWrapper
                .BlobRepository
                .GetById(id);

            return _mapper.Map<BlogDto>(entity);
        }

        public IEnumerable<BlogDto> GetAll(string userId = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return _mapper.Map<List<BlogDto>>(
                    _repositoryWrapper
                        .BlobRepository
                        .GetAll()
                );
            }

            return _mapper.Map<List<BlogDto>>(
                _repositoryWrapper
                    .BlobRepository
                    .FindByCondition(x => x.UserId == userId)
            );
        }

        public IEnumerable<BlogDto> GetAllPending(string userId = null)
        {
            return _mapper.Map<List<BlogDto>>(GetBlogsByStatusAndUserId(BlogStatus.Pending, userId));
        }

        public IEnumerable<BlogDto> GetAllApproved(string userId = null)
        {
            return _mapper.Map<List<BlogDto>>(GetBlogsByStatusAndUserId(BlogStatus.Approved, userId));
        }

        public IEnumerable<BlogDto> GetAllRejected(string userId = null)
        {
            return _mapper.Map<List<BlogDto>>(GetBlogsByStatusAndUserId(BlogStatus.Rejected, userId));
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
            var blog = _repositoryWrapper.BlobRepository.GetById(blogId);

            if (blog == null)
                throw new Exception("Blog not found");

            blog.Status = status;
            _repositoryWrapper.BlobRepository.Update(blog);
            return _mapper.Map<BlogDto>(blog);
        }

        private IEnumerable<Blog> GetBlogsByStatusAndUserId(BlogStatus status, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return _repositoryWrapper.BlobRepository.FindByBlogStatus(status);

            return _repositoryWrapper.BlobRepository.FindByBlogStatusAndUserId(status, userId);
        }

    }
}