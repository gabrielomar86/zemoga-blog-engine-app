using AutoMapper;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.presenter;

namespace BlogEngineApp.services
{
    public class BlogEngineAppMappingProfile : Profile
    {
        public BlogEngineAppMappingProfile()
        {
            CreateMap<Post, PostPresenter>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.SubmitDate, opt => opt.MapFrom(src => src.CreationDate));

            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreationDate, opt => opt.Ignore());

            CreateMap<Comment, CommentDto>()
                .ReverseMap();
            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
