using System;
using System.Collections.Generic;
using BlogEngineApp.core.dto;

namespace BlogEngineApp.core.interfaces.contratos.servicios
{
    public interface IBlogService
    {
        BlogDto GetById(Guid id);
        List<BlogDto> GetAll();
    }
}