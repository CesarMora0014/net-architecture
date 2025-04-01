using AutoMapper;
using MediatR;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.UseCases.Customers.Queries.GetAllCustomerQuery;

public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, Response<IEnumerable<CustomerDTO>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<CustomerDTO>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = new Response<IEnumerable<CustomerDTO>>();

        var customer = await unitOfWork.Customers.GetAllAsync();
        response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customer);

        if (response.Data != null)
        {
            response.IsSuccess = true;
            response.Message = "Consulta Exitosa.";
        }

        return response;
    }
}
