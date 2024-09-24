using Business.Models;

namespace Business.DataRepository
{
    public interface IPageRepository
    {
        Task<bool> AddPageAsync(PageBusiness newPage);
        Task<(List<PageBusiness> Pages, int totalCount)> GetPagedPagesAsync(int bookId, int pageNumber, int pageSize);
        Task<PageBusiness> GetPageAsync(int bookId, int pageId);
        Task<PageBusiness> GetPageByIdAsync(int pageId);
        Task<bool> EditAsync(PageBusiness page);
    }
}
