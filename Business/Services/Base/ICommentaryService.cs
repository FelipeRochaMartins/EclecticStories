using Business.Models;

namespace Business.Services.Base
{
    public interface ICommentaryService
    {
        Task<bool> AddCommentaryAsync(CommentaryBusiness postCmt);
        Task<List<CommentaryBusiness>> GetByBookIdAsync(int bookId);
        Task<bool> DeleteCommentaryAsync(int id);
        Task<string> GetPublisherIdAsync(int id);
    }
}
