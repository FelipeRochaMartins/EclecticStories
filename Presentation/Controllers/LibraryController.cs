using AutoMapper;
using Business.Models;
using Business.Services;
using Business.Services.Base;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Controllers.Base;
using Presentation.Models;
using System.Net;

namespace Presentation.Controllers
{
    public class LibraryController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService; 
        private readonly IBookCoverService _bookCoverService;
        private readonly UserManager<IdentityUser> _userManager;

        public LibraryController(IMapper mapper, IBookService bookService, IBookCoverService bookCoverService, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _bookService = bookService;
            _bookCoverService = bookCoverService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchTerm, int pageNumber = 1, int pageSize = 3)
        {
            ViewData["CurrentFilter"] = searchTerm;

            var (books, totalCount) = await _bookService.GetBooksPagedAsync(searchTerm, pageNumber, pageSize);

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

        [Authorize(Roles = "Admin, Publisher")]
        [HttpGet]
        public IActionResult New()
        {
            return View(new BookViewModel());
        }

        [Authorize(Roles = "Admin, Publisher")]
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

        [HttpGet]
        public async Task<IActionResult> Book(int id)
        {
            BookBusiness? book = await _bookService.GetBookByIdAsync(id);

            if (book != null)
            {
                return View(book);
            }

            PopUpError("Unable To Find This Book.");
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Publisher")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string? userId = _userManager.GetUserId(User);
            string? publisherId = await _bookService.GetPublisherIdAsync(id);

            if (userId != null && publisherId != null)
            {
                if (userId == publisherId || User.IsInRole("Admin"))
                {
                    if (await _bookCoverService.DeleteBookCoverAsync(id))
                    {
                        if (await _bookService.DeleteBookByIdAsync(id))
                        {
                            PopUpInfo("Successfully deleted book");
                            return RedirectToAction(nameof(Index));
                        }
                    }

                    PopUpError("The Book Cannot Be Deleted");
                    return RedirectToAction("Book", "Library", new { id = id });
                }

                PopUpWarning("You are not allowed to delete this book");
                return RedirectToAction("Book", "Library", new { id = id });
            }

            PopUpError("The Book Cannot Be Deleted");
            return RedirectToAction("Book", "Library", new { id = id });
        }

        [Authorize(Roles = "Admin, Publisher")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            BookViewModel book = _mapper.Map<BookViewModel>(await _bookService.GetBookByIdAsync(id));

            return View(book);
        }

        [Authorize(Roles = "Admin, Publisher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookViewModel book)
        {
            string? userId = _userManager.GetUserId(User);
            string? publisherId = await _bookService.GetPublisherIdAsync(book.BookId);

            if (userId != null && publisherId != null)
            {
                if (userId == publisherId || User.IsInRole("Admin"))
                {
                    try
                    {
                        BookBusiness bookToEdit = _mapper.Map<BookBusiness>(book);

                        if (ModelState.IsValid)
                        {
                            if (await _bookService.EditAsync(bookToEdit))
                            {
                                if (book.Cover != null && book.Cover.Length > 0)
                                {
                                    if (await _bookCoverService.EditBookCoverAsync(book.Cover, bookToEdit.BookId))
                                    {
                                        PopUpInfo("The book was edited with his cover");
                                        return RedirectToAction("Book", new { id = bookToEdit.BookId });
                                    }

                                    PopUpWarning("An error occurred while saving the book cover");
                                    PopUpSuccess("The book was successfully edited without his cover.");
                                    return RedirectToAction("Book", new { id = bookToEdit.BookId });
                                }

                                PopUpSuccess("The book was successfully edited without his cover.");
                                return RedirectToAction("Book", new { id = bookToEdit.BookId });
                            }

                            PopUpError("An error occurred while editing the book");
                            return View(book);
                        }

                        PopUpError("An error occurred while editing the book");
                        return View(book);
                    }
                    catch (Exception)
                    {
                        PopUpError("An exception occurred while editing the book");
                        return View(book);
                    }
                }

                PopUpWarning("You are not allowed to edit this book");
                return RedirectToAction("Book", "Library", new { id = book.BookId });
            }

            PopUpError("The Book Cannot Be Edited");
            return RedirectToAction("Book", "Library", new { id = book.BookId });

        }
    }
}
