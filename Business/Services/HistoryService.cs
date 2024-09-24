using Business.DataRepository;
using Business.Models;
using Business.Services.Base;

namespace Business.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public Task<bool> AddHistoryAsync(string userId, int bookId)
        {
            return _historyRepository.AddHistoryAsync(userId, bookId);
        }

        public Task<bool> AddHistoryAsync(string userId, int bookId, int pageNum)
        {
            return _historyRepository.AddHistoryAsync(userId, bookId, pageNum);
        }

        public Task<bool> GetAlreadyExistAsync(string userId, int bookId)
        {
            return _historyRepository.GetAlreadyExistAsync(userId, bookId);
        }

        public Task<List<HistoryBusiness>> GetFavoriteHistoriesAsync(string userId)
        {
            return _historyRepository.GetFavoriteHistoriesAsync(userId);
        }

        public async Task<bool> GetIsFavoriteBookAsync(string userId, int bookId)
        {
            return await _historyRepository.GetIsFavoriteBookAsync(userId, bookId);
        }

        public Task<bool> UpdateFavoriteAsync(string userId, int bookId)
        {
            return _historyRepository.UpdateFavoriteAsync(userId, bookId); 
        }

        public Task<bool> UpdatePageAsync(string userId, int bookId, int pageNum)
        {
            return _historyRepository.UpdatePageAsync(userId, bookId, pageNum);
        }

        public async Task<bool> UpdatePageInHistoryService(string userId, int bookId, int pageNum)
        {
            if (await GetAlreadyExistAsync(userId, bookId))
            {
                return await UpdatePageAsync(userId, bookId, pageNum);
            }
            else
            {
                return await AddHistoryAsync(userId, bookId, pageNum);
            }
        }
    }
}
