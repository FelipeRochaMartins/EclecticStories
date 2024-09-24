using Business.Models;

namespace Business.DataRepository
{
    public interface ICommentaryRepository
    {
        Task<bool> AddCommentaryAsync(CommentaryBusiness postCmt);
        Task<List<CommentaryBusiness>> GetByBookIdAsync(int bookId);
        Task<bool> DeleteCommentaryAsync(int id);
        Task<string> GetPublisherIdAsync(int id);
    }
}
