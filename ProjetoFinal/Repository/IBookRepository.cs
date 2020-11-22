
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
        Book getInfoOneBook(BookVO book);
        List<Book> getInfoAllBook(List<BookVO> books);
    }
}