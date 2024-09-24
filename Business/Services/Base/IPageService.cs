using Business.Models;
using System.Net;

namespace Business.Services.Base
{
    public interface IPageService
    {
        Task<bool> AddPageAsync(PageBusiness newPage);
        Task<PageBusiness> GetPageAsync(int bookId, int pageNum);
        Task<PageBusiness> GetPageByIdAsync(int pageId);
        Task<bool> EditAsync(PageBusiness page);
    }
}
