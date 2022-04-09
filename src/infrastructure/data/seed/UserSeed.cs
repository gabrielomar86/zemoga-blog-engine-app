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
                    UserName = "gtarapues",
                    Password = StringUtil.GetMd5Hash("writer*123"),
                    FullName = "Gabriel Tarapues",
                    Role = Roles.Writer
                },
                new User
                {
                    UserName = "orodriguez",
                    Password = StringUtil.GetMd5Hash("editor*123"),
                    FullName = "Omar Rodriguez",
                    Role = Roles.Editor
                }
            );
        }
    }
}