using Solutis.Model;
using System.Linq;
using Solutis.Repository;

namespace Solutis.Business.Implementations
{
    public  class UserBusinessImplementation : IUserBusiness
    {
        private readonly IRepository<User> _repository;

        public UserBusinessImplementation (IRepository<User> repository)
        {
            _repository = repository;
        }

        public User Validate(string username,string password){
            var users = (_repository.FindAll());
            
            return users.Where(
                    x => x.Username.ToLower() == username.ToLower() && 
                    x.Password == password).FirstOrDefault();
        }
    }
}