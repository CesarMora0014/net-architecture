using MediatR;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Transversal.Common;


namespace Pacagroup.Ecommerce.Application.UseCases.Customers.Commands.DeleteCustomerCommand;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Response<bool>>
{
    private readonly IUnitOfWork unitOfWork;

    public DeleteCustomerHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<bool>();

        response.Data = await unitOfWork.Customers.DeleteAsync(request.CustomerID);

        if (response.Data)
        {
            response.IsSuccess = true;
            response.Message = "Eliminación Exitosa.";
        }

        return response;
    }
}
