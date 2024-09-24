using AutoMapper;
using Business.Models;
using Business.Services;
using Business.Services.Base;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IBookService _bookService;
        
        public PageController(IMapper mapper, UserManager<IdentityUser> userManager, IPageService pageService, IHistoryService historyService, IBookService bookService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _pageService = pageService;
            _historyService = historyService;
            _bookService = bookService;
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

        [Authorize(Roles = "Admin, Publisher")]
        [HttpGet]
        public IActionResult New(int bookId)
        {
            PageViewModel page = new PageViewModel();

            page.BookId = bookId;

            return View(page);
        }

        [Authorize(Roles = "Admin, Publisher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(PageViewModel page)
        {
            string? userId = _userManager.GetUserId(User);
            string? publisherId = await _bookService.GetPublisherIdAsync(page.BookId);

            if (userId != null && publisherId != null)
            {
                if (userId == publisherId || User.IsInRole("Admin"))
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

                PopUpWarning("You don't have permission to create a page for this book");
                return RedirectToAction("Index", "Library");
            }

            PopUpError("The Page Cannot be Created, User Data Invalid");
            return RedirectToAction("Index", "Library");
        }

        [Authorize(Roles = "Admin, Publisher")]
        [HttpGet]
        public async Task<IActionResult> Edit(int pageId)
        {
            PageViewModel pageToEdit = _mapper.Map<PageViewModel>(await _pageService.GetPageByIdAsync(pageId));

            return View(pageToEdit);
        }

        [Authorize(Roles = "Admin, Publisher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PageViewModel editedPage)
        {
            string? userId = _userManager.GetUserId(User);
            string? publisherId = await _bookService.GetPublisherIdAsync(editedPage.BookId);

            if (userId != null && publisherId != null)
            {
                if (userId == publisherId || User.IsInRole("Admin"))
                {
                    try
                    {
                        PageBusiness pageToEdit = _mapper.Map<PageBusiness>(editedPage);

                        if (ModelState.IsValid)
                        {
                            if (await _pageService.EditAsync(pageToEdit))
                            {
                                PopUpSuccess("The page was successfully edited.");
                                return RedirectToAction("Index", new { bookId = pageToEdit.BookId, pageNum = pageToEdit.PageNumber });
                            }

                            PopUpError("An error occurred while editing the page.");
                            return View(editedPage);
                        }

                        PopUpError("An error occurred while editing the page.");
                        return View(editedPage);
                    }
                    catch (Exception)
                    {
                        PopUpError("An exception occurred while editing the page.");
                        return View(editedPage);
                    }
                }

                PopUpWarning("You don't have permission to edit a page in this book");
                return RedirectToAction("Index", "Library");
            }

            PopUpError("The Page Cannot be Edited, User Data Invalid");
            return RedirectToAction("Index", "Library");
        }
    }
}
