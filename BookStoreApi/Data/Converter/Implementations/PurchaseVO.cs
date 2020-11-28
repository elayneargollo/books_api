using Solutis.Data.VO;
using Solutis.Data.Converter.Contract;
using Solutis.Model;
using System.Collections.Generic;
using System.Linq;
using Solutis.Repository;
using Solutis.Business;


namespace Solutis.Data.Converter.Implementations
{
    public class PurchaseConverter : IParser<PurchaseVO, Purchase>, IParser<Purchase, PurchaseVO>
    {

        private readonly IRepository<User> _repository;
        private IBookBusiness _bookBusiness;

        public PurchaseConverter(IRepository<User> repository, IBookBusiness bookBusiness)
        {
            _repository = repository;
            _bookBusiness = bookBusiness;
        }

        public Purchase Parse(PurchaseVO origin)
        {
            if (origin == null) return null;

            return new Purchase
            {
                Id = origin.Id,
                Address = origin.Address,
                Email = origin.Email,
                Smartphone = origin.Smartphone,
                //   User = _repository.FindById(origin.idUser).Username,
                Book = _bookBusiness.FindByID(origin.idBook),
                idBook = origin.idBook,
                idUser = origin.idUser
            };
        }

        public PurchaseVO Parse(Purchase origin)
        {
            if (origin == null) return null;

            return new PurchaseVO
            {
                Id = origin.Id,
                Address = origin.Address,
                Email = origin.Email,
                Smartphone = origin.Smartphone,
                User = _repository.FindById(origin.idUser).Username,
                Book = _bookBusiness.FindByID(origin.idBook),
                idBook = origin.idBook,
                idUser = origin.idUser
            };
        }

        public List<Purchase> Parse(List<PurchaseVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<PurchaseVO> Parse(List<Purchase> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();

        }

    }
}