using AutoMapper;
using Business.DataRepository;
using Business.Models;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> AddCommentaryAsync(CommentaryBusiness postCmt)
        {
            try
            {
                CommentaryModel newCmt = _mapper.Map<CommentaryModel>(postCmt);
                newCmt.CreatedDate = DateTime.Now;

                await _context.Commentary.AddAsync(newCmt);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> GetPublisherIdAsync(int id)
        {
            string? publisherId = await _context.Commentary.Where(c => c.CommentId == id).Select(c => c.PublisherId).FirstOrDefaultAsync();

            if (publisherId != null)
            {
                return publisherId;
            }

            return string.Empty;
        }

        public async Task<List<CommentaryBusiness>> GetByBookIdAsync(int bookId)
        {
            var commentsModel =   await _context.Commentary
                                                .Where(c =>  c.BookId == bookId)
                                                .ToListAsync();

            List<CommentaryBusiness> comments = _mapper.Map<List<CommentaryBusiness>>(commentsModel);

            return comments;
        }

        public async Task<bool> DeleteCommentaryAsync(int id)
        {
            try
            {
                CommentaryModel? commentaryDelete = await _context.Commentary.FirstOrDefaultAsync(c => c.CommentId == id);

                if (commentaryDelete != null)
                {
                    _context.Commentary.Remove(commentaryDelete);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
    }
}
