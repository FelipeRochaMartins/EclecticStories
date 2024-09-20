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
        public PageController(IMapper mapper, UserManager<IdentityUser> userManager, IPageService pageService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _pageService = pageService;
        }

        [HttpGet]
        [Route("Page/{bookId}/{pageNum}")]
        public async Task<IActionResult> Index(int bookId, int pageNum)
        {
            PageViewModel page = _mapper.Map<PageViewModel>(await _pageService.GetPageAsync(bookId, pageNum));

            if (page != null)
            {
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
