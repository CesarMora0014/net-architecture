namespace Pacagroup.Ecommerce.Application.Validator.CustomerDTOValidations;

using FluentValidation;
using Pacagroup.Ecommerce.Application.DTO;

public class UpdateCustomerDTOValidator : AbstractValidator<CustomerDTO>
{
    public UpdateCustomerDTOValidator()
    {
        RuleFor(x => x.CustomerID).NotNull().NotEmpty();
    }
}
