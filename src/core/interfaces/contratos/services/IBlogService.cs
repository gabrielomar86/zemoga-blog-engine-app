using System;
using System.Collections.Generic;
using BlogEngineApp.core.dto;

namespace BlogEngineApp.core.interfaces
{
    public interface IBlogService
    {
        BlogDto GetById(Guid id);
        IEnumerable<BlogDto> GetAll();
        BlogDto Reject(Guid blogId);
        BlogDto Approve(Guid blogId);
        IEnumerable<BlogDto> GetAllPending();
        IEnumerable<BlogDto> GetAllApproved();
        IEnumerable<BlogDto> GetAllRejected();
    }
}