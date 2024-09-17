using AutoMapper;
using Business.DataRepository;
using Data.Context;

namespace Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IMapper _mapper;
        private readonly EStoriesContext _context;

        public BookRepository(IMapper mapper, EStoriesContext context)
        {
            _mapper = mapper;
            _context = context;
        }
    }
}
