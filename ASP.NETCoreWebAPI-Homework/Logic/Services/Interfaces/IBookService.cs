using ASP.NETCoreWebAPI_Homework.Logic.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NETCoreWebAPI_Homework.Logic.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookApiModel> AddBook(BookApiModel book);
        Task<IEnumerable<BookApiModel>> GetBooks(string genre = null);
        Task<BookApiModel> ChangeBook(BookApiModel book);
        Task DeleteBook(int id);
    }
}
