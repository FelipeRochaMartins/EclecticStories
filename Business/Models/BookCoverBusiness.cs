using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Business.Models
{
    public class BookCoverBusiness
    {
        public int CoverId { get; set; }
        public int BookId { get; set; }
        public string CoverPath { get; set; }

        public BookCoverBusiness()
        {
            
        }

        public BookCoverBusiness(int bookId, string coverPath)
        {
            BookId = bookId;
            CoverPath = coverPath;
        }

    }
}
