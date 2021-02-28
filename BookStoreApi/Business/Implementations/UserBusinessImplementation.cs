using Solutis.Model;
using System.Linq;
using Solutis.Repository;
using System.Collections.Generic;
using Solutis.Services;
using System;

namespace Solutis.Business.Implementations
{
    public class UserBusinessImplementation : IUserBusiness
    {
        private readonly IRepository<User> _repository;

        public UserBusinessImplementation(IRepository<User> repository)
        {
            _repository = repository;
        }

        public User Validate(string username, string password)
        {

            var users = (_repository.FindAll());
            string passwordGenerate = SecurityService.GenerateMD5(password);

            return users.Where(
                    x =>
                         x.Username.ToLower() == username.ToLower() &&
                         x.Password == passwordGenerate
                         ).FirstOrDefault();
        }


        public List<User> FindAll()
        {
            return _repository.FindAll();
        }

        public User FindByID(long id)
        {
            return _repository.FindById(id);
        }

        public User Create(User user)
        {
            user.Password = SecurityService.GenerateMD5(user.Password);
            return _repository.Create(user);
        }

        public User Update(User book)
        {
            return _repository.Update(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}