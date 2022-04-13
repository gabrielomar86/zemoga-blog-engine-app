using System;
using System.ComponentModel.DataAnnotations;

namespace BlogEngineApp.core.dto
{
    public class CommentDto
    {

        [Required]
        public string Detail { get; set; }
        public Guid? PostId { get; set; }
        public string UserId { get; set; }

    }
}