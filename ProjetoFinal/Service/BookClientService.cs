using Solutis.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Solutis.Repositories;
using Solutis.Data.VO;

namespace Solutis.Services
{
    public class BookClientService : IBookRepository
    {
        static HttpClient client = new HttpClient();
        static string url = "https://www.googleapis.com/books/v1/volumes?q=isbn:";

        public async Task<JToken> GetBookByISBNtAsync(string isbn)
        {
            string res = "";
            JToken obj = "";
            HttpResponseMessage response = await client.GetAsync(url + isbn);

            if (response.IsSuccessStatusCode)
            {
                res =  response.Content.ReadAsStringAsync().Result;
                obj = JObject.Parse(res)["items"][0]["volumeInfo"];
            }

            return obj;
        }

        public Book getInfoOneBook(BookVO book) 
        {
            JToken obj =  this.GetBookByISBNtAsync(book.Isbn).Result;
            Book livro = new Book();

                livro.Description = obj["description"].ToString();
                livro.Title = obj["title"].ToString();
                livro.Id = book.Id;
                livro.Price = book.Price;
                livro.Category = book.Category;
                livro.Isbn = book.Isbn;
                livro.Authors = obj["authors"].ToObject<List<string>>();

            return livro;
        }
    
        public List<Book> getInfoAllBook(List<BookVO> books) 
        {
            List<Book> livros = new List<Book>();

            foreach (BookVO book in books)
            {
                Book livro = new Book();
                livro = this.getInfoOneBook(book);
                
                livros.Add(livro);
            }

            return livros;
        }
    
    }

}