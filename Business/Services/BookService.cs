using Business.DataRepository;
using Business.Models;
using Business.Services.Base;


namespace Business.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<int> AddAsync(BookBusiness newBook)
        {
            return await _bookRepository.AddAsync(newBook);
        }

        public async Task<(List<BookBusiness> Book, int TotalCount)> GetBooksPagedAsync(string searchTerm, int pageNumber, int pageSize)
        {
            return await _bookRepository.GetBooksPagedAsync(searchTerm, pageNumber, pageSize);
        }

        public async Task<BookBusiness> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        public async Task<bool> DeleteBookByIdAsync(int id)
        {
            return await _bookRepository.DeleteBookByIdAsync(id);
        }

        public async Task<bool> Edit(BookBusiness bookToEdit)
        {
            return await _bookRepository.Edit(bookToEdit);
        }

        public async Task<int> GetTotalPagesAsync(int id)
        {
            return await _bookRepository.GetTotalPagesAsync(id);
        }

        public async Task<bool> UpdatePagesCountAsync(int id, int count)
        {
            return await _bookRepository.UpdatePagesCountAsync(id, count);
        }

        public async Task<string> GetBookNameByIdAsync(int id)
        {
            return await _bookRepository.GetBookNameByIdAsync((int)id);
        }
    }
}
