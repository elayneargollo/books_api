using Solutis.Model;
using FluentValidation;

namespace Solutis.Services
{
    public class UserValidation : AbstractValidator<User> 
    {
        public UserValidation(User user) 
        {
            RuleFor(x => x).NotEmpty().WithMessage("Usuário não pode estar vazio");

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username não pode ser vazio");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password não pode ser vazio");

            RuleFor(x => x.Username).MinimumLength(5).WithMessage("Username deve ter pelo menos 5 caracteres");
            RuleFor(x => x.Password).MinimumLength(5).WithMessage("Password deve ter pelo menos 5 caracteres");

            RuleFor(x => x.Role).MaximumLength(50).WithMessage("Role deve ter no máximo 50 caracteres");

        }
    }
}
