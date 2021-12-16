using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Parser.Manager;

namespace Parser.Api
{

    [ApiController]
    [Route("[controller]")]
    public class FindBookController : ControllerBase
    {
        static readonly ManagerParser s_menager = new();

        public FindBookController()
        {
        }

        [HttpGet]
        public async Task<IEnumerable<BookInfo>>  Get()
        {
            var books = await s_menager.ParseAsync("React");
            return books;
        }

        [HttpGet("search={bookName}")]
        public async Task<IEnumerable<BookInfo>> Get(string bookName)
        {
            var books = await s_menager.ParseAsync(bookName);
            return books;
        }

    }
}
