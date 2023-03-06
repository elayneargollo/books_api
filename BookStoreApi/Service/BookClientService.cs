using Solutis.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Solutis.Repositories;
using Solutis.Data.VO;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Solutis.Model.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Solutis.Services
{
    public class BookClientService : IBookRepository
    {
        private readonly ILogger _logger;
        static HttpClient client = new HttpClient();
        static string url = "https://www.googleapis.com/books/v1/volumes?q=isbn:";

        public BookClientService(ILogger<BookClientService> logger)
        {
            _logger = logger;
        }

        public async Task<JToken> GetBookByISBNtAsync(string isbn)
        {
            _logger.LogInformation("Method GetBookByISBNtAsync in BookClientService");

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
            _logger.LogInformation("Method GetInfoOneBook in BookClientService");

            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }  
        }

        public string GetImagBook(BookVO book) 
        {
            _logger.LogInformation("Method GetImagBook in BookClientService");

            try
            {
                JToken obj = this.GetBookByISBNtAsync(book.Isbn)?.Result;
                var pathImagem = findPathImagem(obj["imageLinks"]?.ToString());            

                return pathImagem;
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Image not found");
            }  
        }

        private string findPathImagem(string imagem)
        {
            _logger.LogInformation("Method findPathImagem in BookClientService");

            if(imagem == null)
            {
                return "";
            }
            
            var paths = JsonConvert.DeserializeObject<Imagem>(imagem);
            return paths.smallThumbnail;
        }
    
        public List<Book> GetInfoAllBook(List<BookVO> books) 
        {
            _logger.LogInformation("Method GetInfoAllBook in BookClientService");

            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in method GetInfoAllBook in BookClientService");
                throw new Exception("Image not found");
            }           
        }
    }

}