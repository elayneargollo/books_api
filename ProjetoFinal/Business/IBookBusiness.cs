using System.Collections.Generic;
using Solutis.Data.VO;

namespace Solutis.Business
{
    public interface IBookBusiness
    {
       BookVO Create(BookVO book);
        BookVO FindByID(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}
