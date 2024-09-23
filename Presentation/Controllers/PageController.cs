using AutoMapper;
using Business.Models;
using Business.Services.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class PageController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPageService _pageService;
        private readonly IHistoryService _historyService;
        
        public PageController(IMapper mapper, UserManager<IdentityUser> userManager, IPageService pageService, IHistoryService historyService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _pageService = pageService;
            _historyService = historyService;
        }

        [HttpGet]
        [Route("Page/{bookId}/{pageNum}")]
        public async Task<IActionResult> Index(int bookId, int pageNum)
        {
            PageViewModel page = _mapper.Map<PageViewModel>(await _pageService.GetPageAsync(bookId, pageNum));

            if (page != null)
            {
                string? userId = _userManager.GetUserId(User);

                if (userId != null)
                {
                    bool result = await _historyService.UpdatePageInHistoryService(userId, bookId, pageNum);

                    if (!result)
                    {
                        PopUpWarning("Due to an error, the page number is not being saved in your history");
                    }
                }

                return View(page);
            }

            PopUpError($"Page number {pageNum.ToString()} not found");
            return RedirectToAction("Book", "Library", new { id = bookId });
        }

        [HttpGet]
        public IActionResult New(int bookId)
        {
            PageViewModel page = new PageViewModel();

            page.BookId = bookId;

            return View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(PageViewModel page)
        {
            try
            {
                if (page.BookId > 0)
                {
                    if (ModelState.IsValid)
                    {
                        PageBusiness newPage = _mapper.Map<PageBusiness>(page);

                        if (await _pageService.AddPageAsync(newPage))
                        {
                            PopUpSuccess("Page created successfully");
                            return RedirectToAction("Book", "Library", new { id = page.BookId });
                        }
                    }
                    PopUpError("Invalid page content");
                    return View(page);
                }
                PopUpError("Unable to create the page, try again");
                return RedirectToAction("Index", "Library");
            }
            catch (Exception)
            {
                if (page.BookId > 0)
                {
                    PopUpError("An Exception was occurred while creating the page, try again");
                    return RedirectToAction("Book", "Library", new { id = page.BookId });
                }

                PopUpError("An Exception was occurred while creating the page, try again");
                return RedirectToAction("Index", "Library");
            }
        }
    }
}
