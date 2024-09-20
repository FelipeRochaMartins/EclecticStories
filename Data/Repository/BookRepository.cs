using AutoMapper;
using Business.DataRepository;
using Business.Models;
using Data.Context;
using Data.Migrations;
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

                bookEntity.CreatedDate = DateTime.Now;

                await _context.Book.AddAsync(bookEntity);

                await _context.SaveChangesAsync();

                return bookEntity.BookId;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<(List<BookBusiness> Book, int TotalCount)> GetBooksPagedAsync(string searchTerm, int pageNumber, int pageSize)
        {
            var query = _context.Book
                .Select(b => new BookBusiness
                {
                    BookId = b.BookId,
                    Name = b.Name
                });

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b => b.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalCount = await query.CountAsync();

            var books = await query
                .OrderByDescending(b => b.BookId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (books, totalCount);
        }

        public async Task<BookBusiness> GetBookByIdAsync(int id)
        {
            BookModel? book = await _context.Book.FirstOrDefaultAsync(b => b.BookId == id);
            return _mapper.Map<BookBusiness>(book);
        }

        public async Task<bool> DeleteBookByIdAsync(int id)
        {
            try
            {
                BookModel? bookDelete = await _context.Book.FirstOrDefaultAsync(b => b.BookId == id);

                if (bookDelete != null)
                {
                    _context.Book.Remove(bookDelete);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Edit(BookBusiness bookToEdit)
        {
            try
            {
                BookModel? bookUpdate = await _context.Book.FirstOrDefaultAsync(b => b.BookId == bookToEdit.BookId);

                if (bookUpdate != null)
                {
                    _mapper.Map(bookToEdit, bookUpdate);

                    bookUpdate.LastUpdateDate = DateTime.Now;

                    _context.Book.Update(bookUpdate);   
                    _context.SaveChanges();

                    return true;    
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> GetTotalPagesAsync(int id)
        {
            int? totalPages = await _context.Book
                                    .Where(b => b.BookId == id)
                                    .Select(b => b.PagesCount)
                                    .FirstOrDefaultAsync();

            if (totalPages == null)
            {
                return 0;
            }

            return totalPages.Value;
        }

        public async Task<bool> UpdatePagesCountAsync(int id, int count)
        {
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return false;
            }

            book.PagesCount = count;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
