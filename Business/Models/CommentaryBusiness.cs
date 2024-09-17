namespace Business.Models
{
    public class CommentaryBusiness
    {
        public int CommentId { get; set; }
        public int PublisherId { get; set; }
        public int BookId { get; set; }
        public string Comment { get; set; }
    }
}
