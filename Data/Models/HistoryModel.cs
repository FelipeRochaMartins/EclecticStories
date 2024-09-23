using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class HistoryModel
    {
        [Key]
        public int HistoryId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual BookModel Book { get; set; }

        public bool IsFavorite { get; set; }

        public int? LastPageRead { get; set; }
    }
}
