using MediatR;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;


namespace Pacagroup.Ecommerce.Application.UseCases.Customers.Queries.GetAllPaginatedCustomerQuery;

public sealed record class GetAllPaginatedCustomerQuery: IRequest<ResponsePagination<IEnumerable<CustomerDTO>>>
{
    public int PageNumber {  get; set; }
    public int PageSize { get; set; }
}
