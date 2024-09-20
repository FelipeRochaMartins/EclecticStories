using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class PageViewModel
    {
        public int PageId { get; set; }
        public int BookId { get; set; }
        public int? PageNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100000)]
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
