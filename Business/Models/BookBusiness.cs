namespace Business.Models
{
    public class BookBusiness
    {
        public int BookId { get; set; }
        public string PublisherId { get; set; }
        public string Name { get; set; }
        public string? Author { get; set; }
        public string? Summary { get; set; }
        public int? PagesCount { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public BookBusiness()
        {
            
        }

        public BookBusiness(string publisherId, string name, string? author, string? summary)
        {
            PublisherId = publisherId;
            Name = name;
            Author = author;
            Summary = summary;
            PagesCount = null;
            LastUpdateDate = DateTime.Now;
        }
    }
}
