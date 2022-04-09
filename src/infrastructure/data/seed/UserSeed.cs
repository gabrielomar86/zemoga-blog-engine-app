using BlogEngineApp.core;
using BlogEngineApp.core.entities;
using BlogEngineApp.core.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogEngineApp.infrastructure.data
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    UserName = "writer",
                    Password = StringUtil.GetMd5Hash("writer"),
                    FullName = "Gabriel Tarapues",
                    Role = Roles.Writer
                },
                new User
                {
                    UserName = "editor",
                    Password = StringUtil.GetMd5Hash("editor"),
                    FullName = "Omar Rodriguez",
                    Role = Roles.Editor
                }
            );
        }
    }
}