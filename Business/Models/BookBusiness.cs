namespace Business.Models
{
    public class BookBusiness
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public string Name { get; set; }
        public string? Author { get; set; }
        public string? Summary { get; set; }
        public int? PagesCount { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
