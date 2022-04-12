using System;
using System.Collections.Generic;
using BlogEngineApp.core.dto;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.presenter;

namespace BlogEngineApp.core.interfaces
{
    public interface IUserService
    {
        bool IsValidatedUser(string username, string password, out User user);
    }
}