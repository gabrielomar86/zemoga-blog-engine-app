using AutoMapper;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.entities;

namespace BlogEngineApp.core.extensions
{
    public class BlogEngineAppMappingProfile : Profile
    {
        public BlogEngineAppMappingProfile()
        {
            CreateMap<Blog, BlogDto>();
        }
    }
}
