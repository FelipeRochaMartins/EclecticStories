namespace Business.Models
{
    public class CommentaryBusiness
    {
        public int CommentId { get; set; }
        public string PublisherId { get; set; }
        public string Username { get; set; }
        public int BookId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
