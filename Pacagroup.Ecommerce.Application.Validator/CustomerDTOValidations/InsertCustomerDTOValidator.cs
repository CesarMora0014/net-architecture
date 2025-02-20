namespace Pacagroup.Ecommerce.Application.Validator.CustomerDTOValidations;

using FluentValidation;
using Pacagroup.Ecommerce.Application.DTO;

public class InsertCustomerDTOValidator : AbstractValidator<CustomerDTO>
{
    public InsertCustomerDTOValidator()
    {
        RuleFor(x => x.CompanyName).NotNull().NotEmpty();
        RuleFor(x => x.ContactName).NotNull().NotEmpty();
        RuleFor(x => x.ContactTitle).NotNull().NotEmpty();
        RuleFor(x => x.Address).NotNull().NotEmpty();
        RuleFor(x => x.City).NotNull().NotEmpty();
        RuleFor(x => x.Region).NotNull().NotEmpty();
        RuleFor(x => x.PostalCode).NotNull().NotEmpty();
        RuleFor(x => x.Country).NotNull().NotEmpty();
        RuleFor(x => x.Phone).NotNull().NotEmpty();
        RuleFor(x => x.Fax).NotNull().NotEmpty();
    }
}
