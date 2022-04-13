using System;
using System.Collections.Generic;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.presenter;

namespace BlogEngineApp.core.interfaces
{
    public interface ICommentService
    {
        IEnumerable<CommentPresenter> GetAllCommentsByPostId(Guid postId);
        CommentPresenter CreateComment(CommentDto commentDto);
    }
}