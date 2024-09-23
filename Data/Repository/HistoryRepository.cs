using AutoMapper;
using Business.DataRepository;
using Business.Models;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly IMapper _mapper;
        private readonly EStoriesContext _context;

        public HistoryRepository(IMapper mapper, EStoriesContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> AddHistoryAsync(string userId, int bookId)
        {
            try
            {
                HistoryModel newHist = new HistoryModel();
                newHist.UserId = userId;
                newHist.BookId = bookId;
                newHist.IsFavorite = true;
                newHist.LastPageRead = null;

                await _context.History.AddAsync(newHist);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddHistoryAsync(string userId, int bookId, int pageNum)
        {
            try
            {
                HistoryModel newHist = new HistoryModel();
                newHist.UserId = userId;
                newHist.BookId = bookId;
                newHist.IsFavorite = false;
                newHist.LastPageRead = pageNum;

                await _context.History.AddAsync(newHist);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> GetAlreadyExistAsync(string userId, int bookId)
        {
            return await _context.History.AnyAsync(h => h.UserId == userId && h.BookId == bookId);
        }

        public async Task<List<HistoryBusiness>> GetFavoriteHistoriesAsync(string userId)
        {
            List<HistoryModel> histModel = await _context.History
                                     .Where(h => h.UserId == userId && h.IsFavorite)
                                     .ToListAsync();

            return _mapper.Map<List<HistoryBusiness>>(histModel);
        }

        public async Task<bool> UpdateFavoriteAsync(string userId, int bookId)
        {
            var history = await _context.History.FirstOrDefaultAsync(h => h.UserId == userId && h.BookId == bookId);

            if (history == null)
                return false;

            history.IsFavorite = !history.IsFavorite;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdatePageAsync(string userId, int bookId, int pageNum)
        {
            var history = await _context.History
                                .FirstOrDefaultAsync(h => h.UserId == userId && h.BookId == bookId);

            if (history == null)
                return false;

            history.LastPageRead = pageNum;

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
