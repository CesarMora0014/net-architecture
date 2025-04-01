using AutoMapper;
using MediatR;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Transversal.Common;


namespace Pacagroup.Ecommerce.Application.UseCases.Customers.Queries.GetAllPaginatedCustomerQuery;

public class GetAllPaginatedCustomerHandler : IRequestHandler<GetAllPaginatedCustomerQuery, ResponsePagination<IEnumerable<CustomerDTO>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper _mapper;

    public GetAllPaginatedCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponsePagination<IEnumerable<CustomerDTO>>> Handle(GetAllPaginatedCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = new ResponsePagination<IEnumerable<CustomerDTO>>();

        var count = await unitOfWork.Customers.CountAsync();
        var customers = await unitOfWork.Customers.GetAllPaginatedAsync(request.PageNumber, request.PageSize);

        response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

        if (response.Data != null)
        {
            response.PageNumber = request.PageNumber;
            response.TotalPages = (int)Math.Ceiling(count / (double)request.PageSize);
            response.TotalCount = count;
            response.IsSuccess = true;
            response.Message = "Operación realizada con éxito.";
        }

        return response;
    }
}
