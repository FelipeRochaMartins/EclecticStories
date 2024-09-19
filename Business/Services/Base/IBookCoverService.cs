using Microsoft.AspNetCore.Http;
using Business.Models;

namespace Business.Services.Base
{
    public interface IBookCoverService
    {
        Task<BookCoverBusiness> SaveBookCoverAsync(IFormFile cover, int bookId);
        Task<string> GetBookCoverAsync(int bookId);
        Task<bool> EditBookCoverAsync(IFormFile cover, int bookId);
        Task<bool> DeleteBookCoverAsync(int id);
    }
}
