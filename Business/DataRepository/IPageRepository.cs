using Business.Models;

namespace Business.DataRepository
{
    public interface IPageRepository
    {
        Task<bool> AddPageAsync(PageBusiness newPage);
        Task<PageBusiness> GetPageAsync(int bookId, int pageId);
    }
}
