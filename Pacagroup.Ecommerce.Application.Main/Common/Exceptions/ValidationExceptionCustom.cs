using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.UseCases.Common.Exceptions;

public class ValidationExceptionCustom: Exception
{
    public IEnumerable<BaseError>? Errors { get; set; }

    public ValidationExceptionCustom(): base("Uno o mas fallos en la validación")
    {
        Errors = new List<BaseError>();
    }

    public ValidationExceptionCustom(IEnumerable<BaseError>? errors): this()
    {
        Errors = errors;
    }
}
