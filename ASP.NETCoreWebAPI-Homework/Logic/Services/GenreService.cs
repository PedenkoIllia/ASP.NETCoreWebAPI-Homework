using ASP.NETCoreWebAPI_Homework.Data.Context;
using ASP.NETCoreWebAPI_Homework.Logic.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreWebAPI_Homework.Logic.Services
{
    class GenreService : IGenreService
    {
        ApplicationDBContext _context;

        public GenreService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetGenres()
        {
            return await _context.Genres.Select(g=>g.Name).AsNoTracking().ToListAsync();
        }
    }
}
