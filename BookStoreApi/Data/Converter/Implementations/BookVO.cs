using Solutis.Data.VO;
using Solutis.Data.Converter.Contract;
using Solutis.Model;
using System.Collections.Generic;
using System.Linq;

namespace Solutis.Data.Converter.Implementations
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if (origin == null) return null;
            return new Book
            {
                Id = origin.Id,
                Isbn = origin.Isbn,
                Category = origin.Category,
                Price = origin.Price,
                Title = origin.Title,
                Amount = origin.Amount,
                Imagem = origin.Imagem

            };
        }

        public BookVO Parse(Book origin)
        {
            if (origin == null) return null;

            return new BookVO
            {
                Id = origin.Id,
                Isbn = origin.Isbn,
                Category = origin.Category,
                Price = origin.Price,
                Title = origin.Title,
                Amount = origin.Amount,
                Imagem = origin.Imagem
            };
        }

        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }


        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();

        }

    }
}