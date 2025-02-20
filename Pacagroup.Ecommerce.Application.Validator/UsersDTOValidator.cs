namespace Pacagroup.Ecommerce.Application.Validator;

using FluentValidation;
using Pacagroup.Ecommerce.Application.DTO;

public class UsersDTOValidator : AbstractValidator<UserDTO>
{
    public UsersDTOValidator()
    {
        RuleFor(u => u.UserName).NotNull().NotEmpty();
        RuleFor(u => u.Password).NotNull().NotEmpty();
    }
}
