using AutoMapper;
using Business.Models;
using Business.Services.Base;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Controllers.Base;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class BookController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService; 
        private readonly IBookCoverService _bookCoverService;
        private readonly UserManager<IdentityUser> _userManager;

        public BookController(IMapper mapper, IBookService bookService, IBookCoverService bookCoverService, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _bookService = bookService;
            _bookCoverService = bookCoverService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 1)
        {
            var (books, totalCount) = await _bookService.GetBooksPagedAsync(pageNumber, pageSize);

            var viewModel = new BookViewModelList
            {
                Books = books,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View(new BookViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(BookViewModel model)
        {
            try
            {
                string? publisherId = _userManager.GetUserId(User);

                if (publisherId != null)
                {
                    if (ModelState.IsValid)
                    {
                        BookBusiness newBook = new BookBusiness(publisherId, model.Name, model.Author, model.Summary);
                        int bookId = await _bookService.AddAsync(newBook);

                        if (bookId > 0)
                        {
                            if (model.Cover != null && model.Cover.Length > 0)
                            {
                                BookCoverBusiness newCover = await _bookCoverService.SaveBookCoverAsync(model.Cover, bookId);

                                if (newCover.CoverPath != null)
                                {
                                    PopUpInfo("The book was created with a cover");
                                    return RedirectToAction(nameof(Index));
                                }

                                PopUpError("An error occurred while saving the book cover");
                            }
                            else
                            {
                                PopUpInfo("The book was created");
                            }

                            return RedirectToAction(nameof(Index));
                        }
                        PopUpError("Error saving book, try again");
                        return View(model);
                    }
                    else
                    {
                        PopUpWarning("Invalid data entered");
                        return View(model);
                    }
                }
                else
                {
                    PopUpWarning("Invalid user");
                    return RedirectToAction(nameof(AccountController.Login), "Account");
                }
            }
            catch (Exception ex)
            {
                PopUpError($"An exception occurred: {ex.Message}");
                return View(model);
            }
        }

    }
}
