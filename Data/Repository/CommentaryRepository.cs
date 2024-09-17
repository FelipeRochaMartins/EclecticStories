using AutoMapper;
using Business.DataRepository;
using Data.Context;

namespace Data.Repository
{
    public class CommentaryRepository : ICommentaryRepository
    {
        private readonly IMapper _mapper;
        private readonly EStoriesContext _context;

        public CommentaryRepository(IMapper mapper, EStoriesContext context)
        {
            _mapper = mapper;
            _context = context;
        }
    }
}
