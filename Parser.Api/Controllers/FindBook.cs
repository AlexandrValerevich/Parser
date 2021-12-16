using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<BookInfo> Get()
        {
            var books = s_menager.Parse("React");
            return books;
        }

        [HttpGet("search={bookName}")]
        public IEnumerable<BookInfo> Get(string bookName)
        {
            var books = s_menager.Parse(bookName);
            return books;
        }

    }
}
