using System.Collections.Generic;
using Solutis.Data.VO;

namespace Solutis.Business
{
    public interface IPurchaseBusiness
    {
        PurchaseVO Create(PurchaseVO purchase);
        PurchaseVO FindByID(long id);
        List<PurchaseVO> FindAll();
        PurchaseVO Update(PurchaseVO purchase);
        void Delete(long id);
    }
}
