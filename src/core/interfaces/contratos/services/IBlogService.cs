using System;
using System.Collections.Generic;
using BlogEngineApp.core.dto;

namespace BlogEngineApp.core.interfaces
{
    public interface IBlogService
    {
        BlogDto GetById(Guid id);
        BlogDto Reject(Guid blogId);
        BlogDto Approve(Guid blogId);
        IEnumerable<BlogDto> GetAll(string userId = null);
        IEnumerable<BlogDto> GetAllPending(string userId = null);
        IEnumerable<BlogDto> GetAllApproved(string userId = null);
        IEnumerable<BlogDto> GetAllRejected(string userId = null);
    }
}