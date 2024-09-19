using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class BookCoverModel
    {
        [Key]
        public int CoverId { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual BookModel Book { get; set; }

        [Required]
        [MaxLength(255)]
        public string CoverPath { get; set; }
    }
}
