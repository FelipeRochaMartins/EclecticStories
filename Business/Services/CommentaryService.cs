using Business.DataRepository;
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
    }
}
