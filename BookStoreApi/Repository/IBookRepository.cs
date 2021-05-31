
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Solutis.Model;
using Solutis.Data.VO;
using System.Collections.Generic;

namespace Solutis.Repositories
{
    public interface IBookRepository
    {
        Task<JToken> GetBookByISBNtAsync(string isbn);
        Book GetInfoOneBook(BookVO book);
        string GetImagBook(BookVO book);
        List<Book> GetInfoAllBook(List<BookVO> books);

    }
}