using Solutis.Model;
using FluentValidation;
using FluentValidation.Validators;
using Solutis.Business;

namespace Solutis.Services
{
    public class LoginValidation : AbstractValidator<UserRequest> 
    {
        public LoginValidation(UserRequest user, IUserBusiness userBusiness) 
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username não pode ser vazio");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password não pode ser vazio");
            RuleFor(x => x.Password).MinimumLength(5).WithMessage("Password deve ter pelo menos 5 caracteres");

            RuleFor(instance => instance)
                    .SetValidator(new UserNotFound(userBusiness, user)).WithMessage("Usuário não encontrado");

            RuleFor(instance => instance)
                    .SetValidator(new TokenInvalid(userBusiness, user)).WithMessage("Não foi possível gerar o token");
        }

        private class UserNotFound : PropertyValidator
        {
            private readonly IUserBusiness _userBusiness;
            private readonly UserRequest _user;
         
            public UserNotFound(IUserBusiness userBusiness, UserRequest user) 
            {
                _userBusiness = userBusiness;
                _user = user;
            }
            protected override bool IsValid(PropertyValidatorContext context)
            {
                return _userBusiness.Validate(_user.Username, _user.Password) != null;
            }
        }

        private class TokenInvalid : PropertyValidator
        {
            private readonly IUserBusiness _userBusiness;
            private readonly UserRequest _user;
         
            public TokenInvalid(IUserBusiness userBusiness, UserRequest user) 
            {
                _userBusiness = userBusiness;
                _user = user;
            }
            protected override bool IsValid(PropertyValidatorContext context)
            {
                var user = _userBusiness.Validate(_user.Username, _user.Password);

                if (user != null)
                {
                   return SecurityService.GenerateToken(user) != null;
                }

                return false;
            
            }
        }
    }
}