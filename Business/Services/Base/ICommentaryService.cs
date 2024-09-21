using Business.Models;

namespace Business.Services.Base
{
    public interface ICommentaryService
    {
        Task<bool> AddCommentaryAsync(CommentaryBusiness postCmt);
        Task<List<CommentaryBusiness>> GetByBookIdAsync(int bookId);
    }
}
