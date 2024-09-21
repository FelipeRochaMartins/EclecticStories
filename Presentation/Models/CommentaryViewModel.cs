

using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class CommentaryViewModel
    {
        public int CommentId { get; set; }
        public string? PublisherId { get; set; }

        public string? Username { get; set; }
        public int BookId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
