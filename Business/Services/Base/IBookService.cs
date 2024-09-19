using Business.Models;

namespace Business.Services.Base
{
    public interface IBookService
    {
        Task<int> AddAsync(BookBusiness newBook);

        Task<(List<BookBusiness> Book, int TotalCount)> GetBooksPagedAsync(int pageNumber, int pageSize);
    }
}
