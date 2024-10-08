﻿using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class PageModel
    {
        [Key]
        public int PageId { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual BookModel Book { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100000)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
