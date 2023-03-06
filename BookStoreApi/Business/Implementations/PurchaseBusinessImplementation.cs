using System.Collections.Generic;
using Solutis.Model;
using Solutis.Repository;
using Solutis.Data.VO;
using Solutis.Data.Converter.Implementations;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Solutis.Business.Implementations
{
    public class PurchaseBusinessImplementation : IPurchaseBusiness
    {
        private readonly IRepository<Purchase> _repository;
        private readonly IRepository<Book> _bookRepository;
        private readonly PurchaseConverter _converter;
        private readonly ILogger _logger;

        public PurchaseBusinessImplementation(IRepository<Purchase> repository, IRepository<User> userRepository, IBookBusiness bookBusiness, IRepository<Book> bookRepository, ILogger<PurchaseBusinessImplementation> logger)
        {
            _repository = repository;
            _converter = new PurchaseConverter(userRepository, bookBusiness);
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public List<PurchaseVO> FindAll()
        {
            _logger.LogInformation("Method FindAll in PurchaseBusinessImplementation");

            try
            {
                var purchaseList = _repository.FindAll();

                if(purchaseList == null)
                    throw new Exception("Purchase not found");

                return _converter.Parse(purchaseList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Unable to perform this operation");
            }  
        }

        public PurchaseVO FindByID(long id)
        {
            _logger.LogInformation($"Method FindByID in PurchaseBusinessImplementation - {id}");

            try
            {
                var purchase = _repository.FindById(id);

                if(purchase == null)
                    throw new Exception("Purchase not found");

                return _converter.Parse(_repository.FindById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Unable to perform this operation");
            }  
        }

        public PurchaseVO Create(PurchaseVO purchase)
        {
            _logger.LogInformation($"Method Create in PurchaseBusinessImplementation - {purchase}");

            try
            {
                var purchaseEntity = _converter.Parse(purchase);
                Book book = _bookRepository.FindById(purchase.idBook);

                if(book == null)
                    throw new Exception("Book not found");

                if(book.Amount > 0)
                {
                    book.Amount = book.Amount - 1;
                    purchaseEntity = _repository.Create(purchaseEntity);
                }

                return _converter.Parse(purchaseEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Unable to perform this operation");
            }  
        }

        public PurchaseVO Update(PurchaseVO purchase)
        {
            try
            { 
                _logger.LogInformation($"Method Update in PurchaseBusinessImplementation - {purchase}");

                if(purchase == null)
                    throw new Exception("Purchase is mandatory");

                var purchaseEntity = _converter.Parse(purchase);
            
                purchaseEntity = _repository.Update(purchaseEntity);
                return _converter.Parse(purchaseEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Unable to perform this operation");
            }   
        }

        public void Delete(long id)
        {
            _logger.LogInformation($"Method Delete in PurchaseBusinessImplementation - Id: {id}");

            try
            {
                var purchase = _repository.FindById(id);
                Book book = _bookRepository.FindById(purchase.idBook);
                book.Amount = book.Amount + 1;

                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("Unable to perform this operation");
            }     
        }
    }
}
