using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Parser
{
    public class BookInfoAdapterToJson
    {
        private List<BookInfo> _bookInfoList;
        private BookInfo[] _bookInfoAsArray => _bookInfoList.ToArray();
        
        public BookInfoAdapterToJson(BookInfo[] bookInfosArray)
        {
            _bookInfoList = bookInfosArray.ToList();
        }

        public string Convert()
        {
            string resultConvert = JsonConvert.SerializeObject(_bookInfoList);
            return resultConvert;
        }
    }
}
