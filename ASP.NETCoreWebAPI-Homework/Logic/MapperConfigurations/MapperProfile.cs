using ASP.NETCoreWebAPI_Homework.Data.Entities;
using ASP.NETCoreWebAPI_Homework.Logic.ApiModels;
using AutoMapper;
using System.Linq;

namespace ASP.NETCoreWebAPI_Homework.Logic.MapperConfigurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Book, BookApiModel>()
                .ForMember(model => model.Genres, opt => opt.MapFrom(book => book.BookGenres.Select(bg => bg.Genre != null ? bg.Genre.Name : null)));
            CreateMap<BookApiModel, Book>()
                .ForMember(book => book.BookGenres, opt => opt.Ignore());
        }
    }
}
