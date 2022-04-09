using BlogEngineApp.core.dto;
using BlogEngineApp.core.interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;

namespace BlogEngineApp.services
{
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public BlogService(IMapper mapper,
                            IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public BlogDto GetById(Guid id)
        {
            var entity = _repositoryWrapper
                .BlogEngineAppRepository
                .FindByCondition(smp => smp.Id == id)
                .FirstOrDefault();
            return _mapper.Map<BlogDto>(entity);
        }

        public List<BlogDto> GetAll()
        {
            var entities = _repositoryWrapper.BlogEngineAppRepository.GetAll();
            return _mapper.Map<List<BlogDto>>(entities);
        }

    }
}