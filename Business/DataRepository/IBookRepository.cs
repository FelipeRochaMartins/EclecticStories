using Business.Models;

namespace Business.DataRepository
{
    public interface IBookRepository
    {
        Task<int> AddAsync(BookBusiness newBook);

        Task<(List<BookBusiness> Book, int TotalCount)> GetBooksPagedAsync(int pageNumber, int pageSize);
    }
}
