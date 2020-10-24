using ASP.NETCoreWebAPI_Homework.Filters;
using ASP.NETCoreWebAPI_Homework.Logic.ApiModels;
using ASP.NETCoreWebAPI_Homework.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NETCoreWebAPI_Homework.Controllers
{
    [ExceptionFilter]
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IEnumerable<BookApiModel>> GetAllBooks([FromQuery] string genre)
        {
            return await _bookService.GetBooks(genre);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookApiModel book)
        {
            book = await _bookService.AddBook(book);

            return Ok(book);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] BookApiModel book)
        {
            book = await _bookService.ChangeBook(book);

            return Ok(book);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook([FromQuery] int id)
        {
            await _bookService.DeleteBook(id);

            return NoContent();
        }
    }
}
