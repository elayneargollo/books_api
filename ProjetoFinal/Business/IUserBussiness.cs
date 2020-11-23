using Solutis.Model;
using System.Collections.Generic;

namespace Solutis.Business
{
    public interface IUserBusiness
    {
        User Validate(string username, string password);

        User Create(User user);
        User FindByID(long id);
        List<User> FindAll();
        User Update(User user);
        void Delete(long id);
    }
}