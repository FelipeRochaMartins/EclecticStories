using Business.Models;
using Data.Models;

namespace Presentation.Models
{
    public class BookViewModelList
    {
        public List<BookBusiness> Books { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
