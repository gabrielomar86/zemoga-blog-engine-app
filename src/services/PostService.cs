using BlogEngineApp.core.dto;
using BlogEngineApp.core.interfaces;
using System;
using System.Collections.Generic;
using AutoMapper;
using BlogEngineApp.core.enums;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.extensions;
using BlogEngineApp.core.presenter;
using BlogEngineApp.core;

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

        public PostDto CreatePost(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            _repositoryWrapper.PostRepository.Insert(post);
            var postDtoResponse = _mapper.Map<PostDto>(post);

            _creationPostFlowNotifier.UpdatePostToCreatedStatus(postDtoResponse);

            return postDtoResponse;
        }

        public PostDto GetPostById(Guid id)
        {
            var entity = _repositoryWrapper
                .PostRepository
                .GetById(id);

            return _mapper.Map<PostDto>(entity);
        }

        public IEnumerable<PostDto> GetAllPosts(string userId = null)
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

        public IEnumerable<PostPresenter> GetAllPostsPending(string userId = null)
        {
            return _mapper.Map<List<PostPresenter>>(GetAllPostsByStatusAndUserId(PostStatus.PostPending, userId));
        }

        public IEnumerable<PostDto> GetAllPostsApproved(string userId = null)
        {
            return _mapper.Map<List<PostDto>>(GetAllPostsByStatusAndUserId(PostStatus.PostApproved, userId));
        }

        public IEnumerable<PostDto> GetAllPostsRejected(string userId = null)
        {
            return _mapper.Map<List<PostDto>>(GetAllPostsByStatusAndUserId(PostStatus.PostRejected, userId));
        }

        public PostDto ChangePostToCreatedStatus(Guid postId)
        {
            var postDto = UpdatePostStatus(postId, PostStatus.PostCreated);
            _creationPostFlowNotifier.PostChangedToCreatedStatus(postDto);
            return postDto;
        }

        public PostDto ChangePostToPendingStatus(Guid postId)
        {
            var postDto = UpdatePostStatus(postId, PostStatus.PostPending);
            _creationPostFlowNotifier.PostChangedToPendingStatus(postDto);
            return postDto;
        }

        public PostDto ChangePostToRejectStatus(Guid postId)
        {
            return UpdatePostStatus(postId, PostStatus.PostRejected);
        }

        public PostDto ChangePostToApproveStatus(Guid postId)
        {
            return UpdatePostStatus(postId, PostStatus.PostApproved);
        }

        private PostDto UpdatePostStatus(Guid postId, PostStatus status)
        {
            var post = _repositoryWrapper.PostRepository.GetById(postId);

            if (post == null)
                throw new NotFoundResponseException("Post not found");

            post.Status = status;
            _repositoryWrapper.PostRepository.Update(post);
            return _mapper.Map<PostDto>(post);
        }

        private IEnumerable<Post> GetAllPostsByStatusAndUserId(PostStatus status, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return _repositoryWrapper.PostRepository.FindPostsByPostStatus(status);

            return _repositoryWrapper.PostRepository.FindPostsByStatusAndUserId(status, userId);
        }

    }
}