using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public IdentityUser Publisher { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string? Author { get; set; }

        [MaxLength(300)]
        public string? Summary { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<ComentaryModel> Commentaries { get; set; }

        public virtual ICollection<PageModel> Pages { get; set; }

        public int? PagesCount { get; set; }
    }
}
