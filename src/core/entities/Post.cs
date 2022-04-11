using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogEngineApp.core.enums;

namespace BlogEngineApp.core.entities
{
    public class Post : BaseEntity<Guid>
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "varchar(300)")]
        public string Content { get; set; }

        public PostStatus? Status { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        #region Foreign Keys

        [ForeignKey("PostId")]
        public virtual ICollection<Comment> Comments { get; set; }

        #endregion

    }
}