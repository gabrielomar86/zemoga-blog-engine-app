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
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CommentService(IMapper mapper,
                            IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public CommentPresenter CreateComment(CommentDto commentDto)
        {
            var post = _repositoryWrapper.PostRepository.GetById(commentDto.PostId.Value);
            if (post == null)
            {
                throw new NotFoundResponseException("Post not found");
            }

            if (post.Status != PostStatus.PostApproved)
            {
                throw new Exception("Post is not in approved status, you can't comment on it");
            }

            var comment = _mapper.Map<Comment>(commentDto);
            _repositoryWrapper.CommentRepository.Insert(comment);
            return _mapper.Map<CommentPresenter>(comment);
        }

        public IEnumerable<CommentPresenter> GetAllCommentsByPostId(Guid postId)
        {
            var comments = _repositoryWrapper.CommentRepository.FindCommentByPostId(postId);
            return _mapper.Map<List<CommentPresenter>>(comments);
        }
    }
}