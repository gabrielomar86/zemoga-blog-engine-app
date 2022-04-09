using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngineApp.core.entities
{
    public class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
            Comments = new HashSet<Comment>();
        }

        [Key]
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Role { get; set; }

        public bool IsWriter()
        {
            return Role.Equals("writer");
        }

        public bool IsEditor()
        {
            return Role.Equals("editor");
        }

        #region Foreign Keys

        [ForeignKey("UserId")]
        public virtual ICollection<Blog> Blogs { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<Comment> Comments { get; set; }

        #endregion

    }
}