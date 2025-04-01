
using FluentValidation;
using MediatR;
using Pacagroup.Ecommerce.Application.UseCases.Common.Exceptions;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.UseCases.Common.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults.Where(r => r.Errors.Any()).SelectMany(r => r.Errors)
                .Select(r => new BaseError() { PropertyMessage = r.PropertyName, PropertyError = r.ErrorMessage })
                .ToList();

            if (failures.Any())
            {
                throw new ValidationExceptionCustom(failures);
            }
        }

        return await next();
    }
}
