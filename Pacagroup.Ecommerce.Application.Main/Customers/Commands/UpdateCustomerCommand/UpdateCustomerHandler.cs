﻿using AutoMapper;
using MediatR;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.UseCases.Customers.Commands.UpdateCustomerCommand;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Response<bool>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<bool>();

        var customer = _mapper.Map<Customer>(request);
        response.Data = await unitOfWork.Customers.UpdateAsync(customer);

        if (response.Data)
        {
            response.IsSuccess = true;
            response.Message = "Actualización Exitosa.";
        }

        return response;
    }
}
