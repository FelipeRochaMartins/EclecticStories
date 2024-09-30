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

        public async Task<bool> EditAsync(PageBusiness page)
        {
            return await _pageRepository.EditAsync(page);
        }

        public async Task<PageBusiness> GetPageAsync(int bookId, int pageNum)
        {
            return await _pageRepository.GetPageAsync(bookId, pageNum);
        }

        public async Task<PageBusiness> GetPageByIdAsync(int pageId)
        {
            return await _pageRepository.GetPageByIdAsync(pageId);
        }

        public async Task<(List<PageBusiness> Pages, int totalCount)> GetPagedPagesAsync(int bookId, int pageNumber, int pageSize)
        {
            return await _pageRepository.GetPagedPagesAsync(bookId, pageNumber, pageSize);
        }

        public async Task<int> GetTotalPagesAsync(int bookId)
        {
            return await _pageRepository.GetTotalPagesAsync(bookId);
        }

        public string TimeAgo(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                DateTime UpdatedTime = (DateTime)dateTime;

                TimeSpan timeSpan = DateTime.Now - UpdatedTime;

                if (timeSpan.TotalSeconds < 60)
                {
                    return $"{timeSpan.Seconds} second{(timeSpan.Seconds == 1 ? "" : "s")} ago";
                }
                else if (timeSpan.TotalMinutes < 60)
                {
                    return $"{timeSpan.Minutes} minute{(timeSpan.Minutes == 1 ? "" : "s")} ago";
                }
                else if (timeSpan.TotalHours < 24)
                {
                    return $"{timeSpan.Hours} hour{(timeSpan.Hours == 1 ? "" : "s")} ago";
                }
                else if (timeSpan.TotalDays < 30)
                {
                    return $"{timeSpan.Days} day{(timeSpan.Days == 1 ? "" : "s")} ago";
                }
                else if (timeSpan.TotalDays < 365)
                {
                    return $"{timeSpan.Days / 30} month{(timeSpan.Days / 30 == 1 ? "" : "s")} ago";
                }
                else
                {
                    return $"{timeSpan.Days / 365} year{(timeSpan.Days / 365 == 1 ? "" : "s")} ago";
                }
            }
            else
            {
                return "Not Found";
            }


        }
        public string LimitString(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Length <= maxLength ? input : input.Substring(0, maxLength) + "...";
        }
    }
}
