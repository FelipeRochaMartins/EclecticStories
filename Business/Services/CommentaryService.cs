using Business.DataRepository;
using Business.Models;
using Business.Services.Base;

namespace Business.Services
{
    public class CommentaryService : ICommentaryService
    {
        private readonly ICommentaryRepository _commentaryRepository;

        public CommentaryService(ICommentaryRepository commentaryRepository)
        {
            _commentaryRepository = commentaryRepository;
        }

        public async Task<bool> AddCommentaryAsync(CommentaryBusiness postCmt)
        {
            return await _commentaryRepository.AddCommentaryAsync(postCmt);
        }

        public async Task<List<CommentaryBusiness>> GetByBookIdAsync(int bookId)
        {
            return await _commentaryRepository.GetByBookIdAsync(bookId);
        }
    }
}
