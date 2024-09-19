using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        public string? PublisherId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name ="Book Title")]
        public string Name { get; set; }

        [MaxLength(50)]
        public string? Author { get; set; }

        [MaxLength(300)]
        public string? Summary { get; set; }

        [Display(Name = "Book Cover")]
        public IFormFile? Cover { get; set; }
    }
}
