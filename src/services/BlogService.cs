using BlogEngineApp.core.dto;
using BlogEngineApp.core.interfaces;
using System;
using System.Collections.Generic;
using AutoMapper;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.extensions;
using BlogEngineApp.core.presenter;

namespace BlogEngineApp.services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ICreationPostFlowNotifier _creationPostFlowNotifier;

        public PostService(IMapper mapper,
                            IRepositoryWrapper repositoryWrapper,
                            ICreationPostFlowNotifier creationPostFlowNotifier)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _creationPostFlowNotifier = creationPostFlowNotifier;
        }

        public PostDto Create(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            _repositoryWrapper.PostRepository.Insert(post);
            var postDtoResponse = _mapper.Map<PostDto>(post);

            _creationPostFlowNotifier.PostCreated(postDtoResponse);

            return postDtoResponse;
        }

        public PostDto GetById(Guid id)
        {
            var entity = _repositoryWrapper
                .PostRepository
                .GetById(id);

            return _mapper.Map<PostDto>(entity);
        }

        public IEnumerable<PostDto> GetAll(string userId = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return _mapper.Map<List<PostDto>>(
                    _repositoryWrapper
                        .PostRepository
                        .GetAll()
                );
            }

            return _mapper.Map<List<PostDto>>(
                _repositoryWrapper
                    .PostRepository
                    .FindByCondition(x => x.UserId == userId)
            );
        }

        public IEnumerable<PostPresenter> GetAllPending(string userId = null)
        {
            return _mapper.Map<List<PostPresenter>>(GetPostsByStatusAndUserId(PostStatus.Pending, userId));
        }

        public IEnumerable<PostDto> GetAllApproved(string userId = null)
        {
            return _mapper.Map<List<PostDto>>(GetPostsByStatusAndUserId(PostStatus.Approved, userId));
        }

        public IEnumerable<PostDto> GetAllRejected(string userId = null)
        {
            return _mapper.Map<List<PostDto>>(GetPostsByStatusAndUserId(PostStatus.Rejected, userId));
        }

        public PostDto Pending(Guid postId)
        {
            var postDto = UpdateStatus(postId, PostStatus.Pending);
            _creationPostFlowNotifier.PostChangedToPending(postDto);
            return postDto;
        }

        public PostDto Reject(Guid postId)
        {
            return UpdateStatus(postId, PostStatus.Rejected);
        }

        public PostDto Approve(Guid postId)
        {
            return UpdateStatus(postId, PostStatus.Approved);
        }

        private PostDto UpdateStatus(Guid postId, PostStatus status)
        {
            var post = _repositoryWrapper.PostRepository.GetById(postId);

            if (post == null)
                throw new Exception("Post not found");

            post.Status = status;
            _repositoryWrapper.PostRepository.Update(post);
            return _mapper.Map<PostDto>(post);
        }

        private IEnumerable<Post> GetPostsByStatusAndUserId(PostStatus status, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return _repositoryWrapper.PostRepository.FindByPostStatus(status);

            return _repositoryWrapper.PostRepository.FindByPostStatusAndUserId(status, userId);
        }

    }
}