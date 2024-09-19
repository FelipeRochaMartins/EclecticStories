using Business.Models;
using Microsoft.AspNetCore.Http;

namespace Business.DataRepository
{
    public interface IBookCoverRepository
    {
        Task<bool> AddAsync(BookCoverBusiness newCover);

        Task<BookCoverBusiness> SaveBookCoverAsync(IFormFile cover, int bookId);

        Task<string> GetCoverPathAsync(int bookId);
    }
}
