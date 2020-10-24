using ASP.NETCoreWebAPI_Homework.Filters;
using ASP.NETCoreWebAPI_Homework.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NETCoreWebAPI_Homework.Controllers
{
    [ExceptionFilter]
    [ApiController]
    [Route("[controller]")]
    public class GenresController : ControllerBase
    {
        IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetAllGenres()
        {
            return await _genreService.GetGenres();
        }

    }
}
