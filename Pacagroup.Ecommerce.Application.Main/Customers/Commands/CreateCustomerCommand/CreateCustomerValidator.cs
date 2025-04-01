
using FluentValidation;

namespace Pacagroup.Ecommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(model => model.CustomerID).NotNull().NotEmpty().MinimumLength(5);
    }
}
