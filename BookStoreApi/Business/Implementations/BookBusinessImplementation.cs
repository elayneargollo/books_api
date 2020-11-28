using System.Collections.Generic;
using Solutis.Model;
using Solutis.Repository;
using Solutis.Data.VO;
using Solutis.Data.Converter.Implementations;
using Microsoft.Data.SqlClient;
using System;
namespace Solutis.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
    
            try {
                bookEntity = _repository.Create(bookEntity);

            }catch (SqlException odbcEx) {  
                return null;
            }  
            catch (Exception ex) {  
                return null;
            }  

            return _converter.Parse(bookEntity);
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);

            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
