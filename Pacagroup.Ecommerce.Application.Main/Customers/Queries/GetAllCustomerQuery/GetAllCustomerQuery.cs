﻿using MediatR;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.UseCases.Customers.Queries.GetAllCustomerQuery;

public sealed record GetAllCustomerQuery: IRequest<Response<IEnumerable<CustomerDTO>>>
{
}
