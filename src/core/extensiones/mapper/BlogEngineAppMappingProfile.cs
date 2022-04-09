using AutoMapper;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.entities;

namespace BlogEngineApp.core.extensiones
{
    public class BlogEngineAppMappingProfile : Profile
    {
        public BlogEngineAppMappingProfile()
        {
            CreateMap<Blog, BlogDto>();
        }
    }
}
