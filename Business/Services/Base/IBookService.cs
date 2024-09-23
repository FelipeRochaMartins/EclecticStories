using Business.Models;

namespace Business.Services.Base
{
    public interface IBookService
    {
        Task<int> AddAsync(BookBusiness newBook);
        Task<(List<BookBusiness> Book, int TotalCount)> GetBooksPagedAsync(string searchTerm, int pageNumber, int pageSize);
        Task<BookBusiness> GetBookByIdAsync(int id);
        Task<bool> DeleteBookByIdAsync(int id);
        Task<bool> Edit(BookBusiness bookToEdit);
        Task<int> GetTotalPagesAsync(int id);
        Task<bool> UpdatePagesCountAsync(int id, int count);
        Task<string> GetBookNameByIdAsync(int id);
    }
}
