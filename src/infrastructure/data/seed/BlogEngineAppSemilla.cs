using BlogEngineApp.core.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogEngineApp.infrastructure.data
{
    public class BlogSeed : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            // builder.HasData(
            //     new Blog
            //     {
            //         Id = System.Guid.Parse("44ba65f8-ec30-4092-8beb-b952bf0c30df"),

            //         Titulo = "mockData1",

            //         Contenido = "mockData1",

            //         Valor = 123

            //     },
            //     new Blog
            //     {
            //         Id = System.Guid.Parse("ad1100bb-ee24-4444-8289-dfe712891324"),

            //         Nombre = "mockData2",

            //         Descripcion = "mockData2",

            //         Valor = 456

            //     },
            //     new Blog
            //     {
            //         Id = System.Guid.Parse("0a696949-c4f3-4397-b330-b978ee48801d"),

            //         Nombre = "mockData3",

            //         Descripcion = "mockData3",

            //         Valor = 678

            //     }
            // );
        }
    }
}