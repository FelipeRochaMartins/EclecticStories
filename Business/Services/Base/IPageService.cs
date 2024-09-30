using Business.Models;
using System.Net;

namespace Business.Services.Base
{
    public interface IPageService
    {
        Task<bool> AddPageAsync(PageBusiness newPage);
        Task<(List<PageBusiness> Pages, int totalCount)> GetPagedPagesAsync(int bookId, int pageNumber, int pageSize);
        Task<PageBusiness> GetPageAsync(int bookId, int pageNum);
        Task<PageBusiness> GetPageByIdAsync(int pageId);
        Task<int> GetTotalPagesAsync(int bookId);
        Task<bool> EditAsync(PageBusiness page);

        string TimeAgo(DateTime? dateTime);

        string LimitString(string input, int maxLength);
    }
}
