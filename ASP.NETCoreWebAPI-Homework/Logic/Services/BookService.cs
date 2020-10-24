using ASP.NETCoreWebAPI_Homework.Data.Context;
using ASP.NETCoreWebAPI_Homework.Data.Entities;
using ASP.NETCoreWebAPI_Homework.Logic.ApiModels;
using ASP.NETCoreWebAPI_Homework.Logic.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreWebAPI_Homework.Logic.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public BookService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookApiModel> AddBook(BookApiModel book)
        {

            var newBook = _mapper.Map<Book>(book);
            var genreIds = await GetGenreIdFromName(book.Genres);
            foreach (int genreId in genreIds)
                newBook.BookGenres.Add(new BookGenre { BookId = newBook.Id, GenreId = genreId });

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            newBook = await _context.Books.Include(b => b.BookGenres).ThenInclude(bg => bg.Genre).Where(b => b.Id == newBook.Id).AsNoTracking().FirstAsync();
            return _mapper.Map<BookApiModel>(newBook);

        }

        public async Task<BookApiModel> ChangeBook(BookApiModel book)
        {
            var changedBook = await _context.Books.Include(b => b.BookGenres).ThenInclude(bg => bg.Genre).Where(b => b.Id == book.Id).AsNoTracking().FirstAsync();
            if (changedBook == null)
                throw new InvalidDataException($"Can't find book with id: {book.Id}");

            var newGenres = await GetGenreIdFromName(book.Genres);
            var oldGenres = changedBook.BookGenres.Select(bg => bg.Genre.Id).ToList();

            await using var transaction = await _context.Database.BeginTransactionAsync();
            {
                var bookGenresRemove = oldGenres.Except(newGenres).Select(gid => new BookGenre { BookId = book.Id, GenreId = gid });
                var bookGenresAdd = newGenres.Except(oldGenres).Select(gid => new BookGenre { BookId = book.Id, GenreId = gid });

                _context.BookGenres.RemoveRange(bookGenresRemove);
                await _context.BookGenres.AddRangeAsync(bookGenresAdd);

                changedBook = _mapper.Map(book, changedBook);
                _context.Entry(changedBook).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            } await transaction.CommitAsync();

            changedBook = await _context.Books.Include(b => b.BookGenres).ThenInclude(bg => bg.Genre).Where(b => b.Id == changedBook.Id).AsNoTracking().FirstAsync();

            return _mapper.Map<BookApiModel>(changedBook);
        }

        public async Task<IEnumerable<BookApiModel>> GetBooks(string genre = null)
        {
            return _mapper.Map<IEnumerable<BookApiModel>>(await _context.Books
                    .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                    .Where(b => (genre == null || b.BookGenres.Where(bg => bg.Genre.Name.ToLower() == genre.ToLower()).Any()))
                    .AsNoTracking()
                    .ToListAsync());
        }

        public async Task DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if(book == null)
                throw new InvalidDataException($"Can't find book with id: {id}");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        private async Task<IEnumerable<int>> GetGenreIdFromName(IEnumerable<string> genreNames)
        {
            List<int> result = new List<int>(genreNames.Count());
            foreach (string genreName in genreNames)
            {
                Genre genre = await _context.Genres.Where(g => g.Name == genreName).AsNoTracking().FirstOrDefaultAsync();
                if (genre != null)
                    result.Add(genre.Id);
                else
                    throw new InvalidDataException($"Can't find genre with name: {genreName}");
            }

            return result;
        }
    }
}
