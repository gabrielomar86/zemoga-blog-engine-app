using System;

namespace BlogEngineApp.core.dto
{
    public class BlogDto
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; } = true;

    }
}