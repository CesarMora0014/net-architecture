using AutoMapper;
using MediatR;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Transversal.Common;


namespace Pacagroup.Ecommerce.Application.UseCases.Customers.Queries.GetCustomerQuery;

public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, Response<CustomerDTO>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<CustomerDTO>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = new Response<CustomerDTO>();

        var customer = await unitOfWork.Customers.GetAsync(request.CustomerId);
        response.Data = _mapper.Map<CustomerDTO>(customer);

        if (response.Data != null)
        {
            response.IsSuccess = true;
            response.Message = "Consulta Exitosa.";
        }

        return response;
    }
}
