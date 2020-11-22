using Solutis.Model;

namespace Solutis.Business
{
    public interface IUserBusiness
    {
        User Validate(string username, string password);
    }
}