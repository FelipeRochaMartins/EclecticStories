using Business.DataRepository;
using Business.Models;
using Business.Services.Base;
using Microsoft.AspNetCore.Http;

namespace Business.Services
{
    public class BookCoverService : IBookCoverService
    {
        private readonly IBookCoverRepository _bookCoverRepository;
        
        public BookCoverService(IBookCoverRepository bookCoverRepository)
        {
            _bookCoverRepository = bookCoverRepository;
        }

        public async Task<string> GetBookCoverAsync(int bookId)
        {
            return await _bookCoverRepository.GetCoverPathAsync(bookId);
        }

        public async Task<BookCoverBusiness> SaveBookCoverAsync(IFormFile cover, int bookId)
        {
            return await _bookCoverRepository.SaveBookCoverAsync(cover, bookId);
        }
    }
}
