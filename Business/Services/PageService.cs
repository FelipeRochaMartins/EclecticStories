using Business.DataRepository;
using Business.Models;
using Business.Services.Base;

namespace Business.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;

        public PageService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<bool> AddPageAsync(PageBusiness newPage)
        {
            return await _pageRepository.AddPageAsync(newPage);
        }

        public async Task<PageBusiness> GetPageAsync(int bookId, int pageNum)
        {
            return await _pageRepository.GetPageAsync(bookId, pageNum);
        }
    }
}
