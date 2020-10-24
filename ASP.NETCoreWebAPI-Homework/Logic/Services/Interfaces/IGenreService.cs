using ASP.NETCoreWebAPI_Homework.Logic.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NETCoreWebAPI_Homework.Logic.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<string>> GetGenres();
    }
}
