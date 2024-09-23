namespace Business.Models
{
    public class HistoryBusiness
    {
        public int HistoryId { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public bool IsFavorite { get; set; }
        public int? LastPageRead { get; set; }
    }
}
