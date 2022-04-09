using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngineApp.core.entities
{
    public class Comment : BaseEntity<Guid>
    {

        [Required]
        [Column(TypeName = "varchar(300)")]
        public string Detail { get; set; }

        public Guid PostId { get; set; }
        public virtual Post Blog { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

    }
}