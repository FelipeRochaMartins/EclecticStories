using AutoMapper;
using Business.Models;
using Data.Models;
using Presentation.Models;

namespace Presentation.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<BookBusiness, BookViewModel>().ReverseMap();
            CreateMap<BookBusiness, BookModel>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<BookCoverBusiness, BookCoverModel>().ReverseMap();
        }
    }
}
