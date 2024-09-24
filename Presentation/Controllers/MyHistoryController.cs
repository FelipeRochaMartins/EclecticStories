using AutoMapper;
using Business.Models;
using Business.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;
using Presentation.Models;

namespace Presentation.Controllers
{
    [Authorize]
    public class MyHistoryController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHistoryService _historyService;

        public MyHistoryController(IMapper mapper, UserManager<IdentityUser> userManager, IHistoryService historyService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _historyService = historyService;   
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = _userManager.GetUserId(User);
            
            if (userId != null)
            {
                List<HistoryBusiness> favoritesBus = await _historyService.GetFavoriteHistoriesAsync(userId);

                if (favoritesBus != null)
                {
                    List<HistoryViewModel> favorites = _mapper.Map<List<HistoryViewModel>>(favoritesBus);

                    return View(favorites);
                }

                PopUpInfo("There are no books saved as Favorite");
                return RedirectToAction("Index", "Library");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Favorite(int bookId)
        {
            string? userId = _userManager.GetUserId(User);

            if (userId != null)
            {
                if (await _historyService.GetAlreadyExistAsync(userId, bookId))
                {
                    if (await _historyService.UpdateFavoriteAsync(userId, bookId))
                    {
                        PopUpInfo("Book removed/added to Your Favorites"); ;
                        return RedirectToAction("Book", "Library", new { id = bookId });
                    }

                    PopUpSuccess("Unable to add/remove this book from Your Favorites, try again");
                    return RedirectToAction("Book", "Library", new { id = bookId });
                }
                else
                {
                    if (await _historyService.AddHistoryAsync(userId, bookId))
                    {
                        PopUpSuccess("Book added to Your Favorites");
                        return RedirectToAction("Book", "Library", new { id = bookId });
                    }

                    PopUpSuccess("Unable to add this book to Your Favorites, try again");
                    return RedirectToAction("Book", "Library", new { id = bookId });
                }
            }

            PopUpWarning("Invalid User action, please login and try again");
            return RedirectToAction("Login", "Account");
        }
    }
}
