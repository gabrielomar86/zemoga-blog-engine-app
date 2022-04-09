using System;
using BlogEngineApp.core.enums;

namespace BlogEngineApp.core.dto
{
    public class UserDto
    {

        public string UserName { get; set; }
        public Roles Role { get; set; }

    }
}