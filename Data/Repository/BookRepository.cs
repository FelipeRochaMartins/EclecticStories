using AutoMapper;
using Business.DataRepository;
using Business.Models;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IMapper _mapper;
        private readonly EStoriesContext _context;

        public BookRepository(IMapper mapper, EStoriesContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> AddAsync(BookBusiness newBook)
        {
            try
            {
                var bookEntity = _mapper.Map<BookModel>(newBook);

                await _context.Book.AddAsync(bookEntity);

                await _context.SaveChangesAsync();

                return bookEntity.BookId;
            }
            catch (Exception)
            {
                return 0;
            } 
        }

        public async Task<(List<BookBusiness> Book, int TotalCount)> GetBooksPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Book
                .Select(b => new BookBusiness
                {
                    BookId = b.BookId,
                    Name = b.Name
                });

            var totalCount = await query.CountAsync();

            var books = await query
                .OrderByDescending(b => b.BookId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (books, totalCount);
        }
    }
}
