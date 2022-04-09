using System;

namespace BlogEngineApp.core.dto
{
    public class CommentDto
    {

        public Guid Id { get; set; }
        public string Detail { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; } = true;

    }
}