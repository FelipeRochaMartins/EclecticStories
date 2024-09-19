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

        public async Task<(List<BookBusiness> Book, int TotalCount)> GetBooksPagedAsync(int pageNumber, int pageSize)
        {
            return await _bookRepository.GetBooksPagedAsync(pageNumber, pageSize);
        }
    }
}
