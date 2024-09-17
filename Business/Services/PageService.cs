using Business.DataRepository;
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
    }
}
