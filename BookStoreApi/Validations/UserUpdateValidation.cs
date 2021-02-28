using Solutis.Model;
using FluentValidation;
using FluentValidation.Validators;
using Solutis.Business;
using System.Linq;

namespace Solutis.Services
{
    public class UserUpdateValidation : AbstractValidator<User> 
    {
        public UserUpdateValidation(User user, IUserBusiness userBusiness) 
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username não pode ser vazio");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password não pode ser vazio");
            RuleFor(x => x.Password).MinimumLength(5).WithMessage("Password deve ter pelo menos 5 caracteres");

            RuleFor(instance => instance)
                    .SetValidator(new UserNotFound(userBusiness, user)).WithMessage("Usuário não encontrado");
        }

        private class UserNotFound : PropertyValidator
        {
            private readonly IUserBusiness _userBusiness;
            private readonly User _user;
         
            public UserNotFound(IUserBusiness userBusiness, User user) 
            {
                _userBusiness = userBusiness;
                _user = user;
            }
            protected override bool IsValid(PropertyValidatorContext context)
            {
                return _userBusiness.FindAll().FirstOrDefault(b => b.Id == _user.Id) != null;
            }
        }
    }
}