using Business.Models;

namespace Business.Services.Base
{
    public interface IHistoryService
    {
        Task<bool> AddHistoryAsync(string userId, int bookId);
        Task<bool> AddHistoryAsync(string userId, int bookId, int pageNum);
        Task<bool> GetAlreadyExistAsync(string userId, int bookId);
        Task<List<HistoryBusiness>> GetFavoriteHistoriesAsync(string userId);
        Task<bool> GetIsFavoriteBookAsync(string userId, int bookId);
        Task<int> GetLastPageReadAsync(string userId, int bookId);
        Task<bool> UpdatePageAsync(string userId, int bookId, int pageNum);
        Task<bool> UpdateFavoriteAsync(string userId, int bookId);
        Task<bool> UpdatePageInHistoryService(string userId, int bookId, int pageNum);
    }
}
