using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class CommentaryModel
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string PublisherId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual BookModel Book { get; set; }

        [Required]
        [MaxLength(200)]
        public string Comment { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("PublisherId")]
        public IdentityUser User { get; set; }
    }
}
