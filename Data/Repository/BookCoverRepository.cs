using AutoMapper;
using Business.DataRepository;
using Business.Models;
using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class BookCoverRepository : IBookCoverRepository
    {
        private readonly IMapper _mapper;
        private readonly EStoriesContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookCoverRepository(IMapper mapper, EStoriesContext context, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> AddAsync(BookCoverBusiness newCover)
        {

            try
            {
                await _context.BookCover.AddAsync(_mapper.Map<BookCoverModel>(newCover));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> GetCoverPathAsync(int bookId)
        {
            string? coverName = await _context.BookCover.Where(bc => bc.BookId == bookId).Select(bc => bc.CoverPath).FirstOrDefaultAsync();

            if (coverName != null)
            {
                return coverName;
            }

            return string.Empty;
        }

        public async Task<BookCoverBusiness> SaveBookCoverAsync(IFormFile cover, int bookId)
        {
            BookCoverBusiness bookCover = new BookCoverBusiness();

            bookCover.BookId = bookId;

            if (cover != null && cover.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(cover.FileName);

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var image = Image.Load<Rgba32>(cover.OpenReadStream()))
                {
                    int newWidth = 300;
                    int newHeight = (int)(newWidth / 3.0 * 5.0);

                    image.Mutate(x => x.Resize(newWidth, newHeight));

                    await image.SaveAsync(filePath, new JpegEncoder());
                }

                bookCover.CoverPath = fileName;

                if (await AddAsync(bookCover))
                {
                    return bookCover;
                }
            }

            return bookCover;
        }
    }
}
