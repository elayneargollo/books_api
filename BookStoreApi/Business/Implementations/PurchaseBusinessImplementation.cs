using System.Collections.Generic;
using Solutis.Model;
using Solutis.Repository;
using Solutis.Data.VO;
using Solutis.Data.Converter.Implementations;

namespace Solutis.Business.Implementations
{
    public class PurchaseBusinessImplementation : IPurchaseBusiness
    {
        private readonly IRepository<Purchase> _repository;
        private readonly IRepository<Book> _bookRepository;
        private readonly PurchaseConverter _converter;

        public PurchaseBusinessImplementation(IRepository<Purchase> repository, IRepository<User> userRepository, IBookBusiness bookBusiness, IRepository<Book> bookRepository)
        {
            _repository = repository;
            _converter = new PurchaseConverter(userRepository, bookBusiness);
            _bookRepository = bookRepository;
        }

        public List<PurchaseVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PurchaseVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PurchaseVO Create(PurchaseVO purchase)
        {
            var purchaseEntity = _converter.Parse(purchase);

            Book book = _bookRepository.FindById(purchase.idBook);

            if(book.Amount > 0)
            {
                book.Amount = book.Amount - 1;
                purchaseEntity = _repository.Create(purchaseEntity);
                return _converter.Parse(purchaseEntity);
            }
            
                return null;
           
        }

        public PurchaseVO Update(PurchaseVO purchase)
        {
            var purchaseEntity = _converter.Parse(purchase);
            
            purchaseEntity = _repository.Update(purchaseEntity);
            return _converter.Parse(purchaseEntity);
        }

        public void Delete(long id)
        {
            
            var purchase = _repository.FindById(id);
            Book book = _bookRepository.FindById(purchase.idBook);
            book.Amount = book.Amount + 1;

            _repository.Delete(id);
        }
    }
}
