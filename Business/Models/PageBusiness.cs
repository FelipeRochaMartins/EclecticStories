﻿namespace Business.Models
{
    public class PageBusiness
    {
        public int PageId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
