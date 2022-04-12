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
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public bool IsValidatedUser(string username, string password, out User user)
        {
            var passwordEncrypted = StringUtil.GetMd5Hash(password);
            user = _repositoryWrapper.UserRepository.ValidateUser(username, passwordEncrypted);
            return user != null;
        }

    }
}