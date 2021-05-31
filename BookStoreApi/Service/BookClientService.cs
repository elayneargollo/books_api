using Solutis.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Solutis.Repositories;
using Solutis.Data.VO;
using Newtonsoft.Json;

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

        public Book GetInfoOneBook(BookVO book) 
        {
            JToken obj =  this.GetBookByISBNtAsync(book.Isbn).Result;
            Book newBook = new Book();

                newBook.Description = obj["description"].ToString();
                newBook.Title = book.Title;
                newBook.Id = book.Id;
                newBook.Price = book.Price;
                newBook.Category = book.Category;
                newBook.Amount = book.Amount;
                newBook.Isbn = book.Isbn;
                newBook.Authors = obj["authors"].ToObject<List<string>>();
                newBook.Imagem = findPathImagem(obj["imageLinks"].ToString());            

            return newBook;
        }

        public string GetImagBook(BookVO book) 
        {
            JToken obj =  this.GetBookByISBNtAsync(book.Isbn)?.Result;
      
            var pathImagem = findPathImagem(obj["imageLinks"]?.ToString());            

            return pathImagem;
        }

        private string findPathImagem(string imagem)
        {
            if(imagem == null)
            {
                return "";
            }
            
            var paths = JsonConvert.DeserializeObject<Imagem>(imagem);
            return paths.smallThumbnail;
        }
    
        public List<Book> GetInfoAllBook(List<BookVO> books) 
        {
            List<Book> newBooks = new List<Book>();

            foreach (BookVO book in books)
            {
                Book newBook = new Book();
                newBook = this.GetInfoOneBook(book);
                
                newBooks.Add(newBook);
            }

            return newBooks;
        }
    
    }

}