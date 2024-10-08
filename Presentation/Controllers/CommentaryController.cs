﻿using AutoMapper;
using Business.Models;
using Business.Services;
using Business.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;
using Presentation.Models;
using System.Xml.Linq;

namespace Presentation.Controllers
{
    public class CommentaryController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICommentaryService _commentaryService;

        public CommentaryController(IMapper mapper, UserManager<IdentityUser> userManager, ICommentaryService commentaryService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _commentaryService = commentaryService;
        }

        [HttpGet]
        [Route("Commentaries/{bookId}/")]
        public async Task<IActionResult> Index(int bookId)
        {
            List<CommentaryBusiness> cmt = await _commentaryService.GetByBookIdAsync(bookId);
            ViewBag.BookId = bookId;

            if (cmt == null)
            {
                return View(new List<CommentaryViewModel>());
            }

            List<CommentaryViewModel> cmtViewModel = _mapper.Map<List<CommentaryViewModel>>(cmt);

            return View(cmtViewModel);
        }

        [Authorize(Roles = "Admin, Publisher, User")]
        [HttpGet]
        public IActionResult New(int bookId)
        {
            CommentaryViewModel comment = new CommentaryViewModel();
            comment.BookId = bookId;
            comment.PublisherId = _userManager.GetUserId(User);
            comment.Username = _userManager.GetUserName(User);

            if (comment.PublisherId != null || comment.Username != null)
            {
                return View(comment);
            }

            PopUpWarning("Invalid User");
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [Authorize(Roles = "Admin, Publisher, User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(CommentaryViewModel cmt)
        {
            try
            {
                if (cmt.BookId > 0)
                {
                    if (ModelState.IsValid || cmt.Username != null || cmt.PublisherId != null)
                    {
                        CommentaryBusiness postCmt = _mapper.Map<CommentaryBusiness>(cmt);

                        if (await _commentaryService.AddCommentaryAsync(postCmt))
                        {
                            PopUpSuccess("Comment Posted");
                            return RedirectToAction("Index", new { bookId = cmt.BookId });
                        }

                        PopUpError("Unable to publish comment, try again");
                        return RedirectToAction("Index", new { bookId = cmt.BookId });
                    }

                    PopUpWarning("Invalid comment");
                    return View(cmt);
                }

                PopUpError("Unable to post the comment, try again");
                return RedirectToAction("Index", "Library");
            }
            catch (Exception)
            {
                if (cmt.BookId > 0)
                {
                    PopUpError("An Exception was occurred while posting the comment, try again");
                    return RedirectToAction("Index", new { bookId = cmt.BookId });
                }

                PopUpError("An Exception was occurred while posting the comment, try again");
                return RedirectToAction("Index", "Library");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id, int bId)
        {
            string? userId = _userManager.GetUserId(User);
            string? publisherId = await _commentaryService.GetPublisherIdAsync(id);

            if (userId != null && publisherId != null)
            {
                if (userId == publisherId || User.IsInRole("Admin"))
                {
                    if (await _commentaryService.DeleteCommentaryAsync(id))
                    {
                        PopUpInfo("Comment deleted");
                        return RedirectToAction("Index", "Commentary", new { bookId = bId });
                    }

                    PopUpError("The Comment Cannot Be Deleted");
                    return RedirectToAction("Index", "Commentary", new { bookId = bId });
                }

                PopUpWarning("You are not allowed to delete this comment");
                return RedirectToAction("Index", "Commentary", new { bookId = bId });
            }

            PopUpError("The Comment Cannot Be Deleted");
            return RedirectToAction("Index", "Commentary", new { bookId = bId });
        }
    }
}
