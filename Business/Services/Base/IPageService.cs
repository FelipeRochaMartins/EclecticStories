using Business.Models;
using System.Net;

namespace Business.Services.Base
{
    public interface IPageService
    {
        Task<bool> AddPageAsync(PageBusiness newPage);
        Task<PageBusiness> GetPageAsync(int bookId, int pageNum);
    }
}
