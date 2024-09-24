using AutoMapper;
using Business.DataRepository;
using Business.Models;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class PageRepository : IPageRepository
    {
        private readonly IMapper _mapper;
        private readonly EStoriesContext _context;
        private readonly IBookRepository _bookRepository;

        public PageRepository(IMapper mapper, EStoriesContext context, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _context = context;
            _bookRepository = bookRepository;
        }

        public async Task<bool> AddPageAsync(PageBusiness newPage)
        {
            try
            {
                int bId = newPage.BookId;

                int pageNumber = await _bookRepository.GetTotalPagesAsync(bId) + 1;

                PageModel pageModel = _mapper.Map<PageModel>(newPage);
                pageModel.PageNumber = pageNumber;
                pageModel.CreatedDate = DateTime.Now;

                await _context.Page.AddAsync(pageModel);
                await _context.SaveChangesAsync();

                await _bookRepository.UpdatePagesCountAsync(bId, pageNumber);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<PageBusiness> GetPageAsync(int bookId, int pageNum)
        {
            PageModel? page = await _context.Page
                                    .Where(p => p.BookId == bookId && p.PageNumber == pageNum)
                                    .FirstOrDefaultAsync();

            return _mapper.Map<PageBusiness>(page);
        }

        public async Task<PageBusiness> GetPageByIdAsync(int pageId)
        {
            PageModel? page = await _context.Page
                                    .Where(p => p.PageId == pageId)
                                    .FirstOrDefaultAsync();

            return _mapper.Map<PageBusiness>(page);
        }

        public async Task<bool> EditAsync(PageBusiness page)
        {
            try
            {
                PageModel? pageUpdate = await _context.Page.FirstOrDefaultAsync(p => p.PageId == page.PageId);

                if (pageUpdate != null)
                {
                    _mapper.Map(page, pageUpdate);

                    _context.Page.Update(pageUpdate);
                    _context.SaveChanges();

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
