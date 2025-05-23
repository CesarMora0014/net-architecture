﻿using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Infrastructure;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Domain.Events;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.UseCases.Discounts;

public class DiscountApplication : IDiscountApplication
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IEventBus eventBus;
    public DiscountApplication(IUnitOfWork unitOfWork, IMapper mapper, IEventBus eventBus)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.eventBus = eventBus;
    }

    public async Task<Response<bool>> Create(DiscountDTO discountDTO, CancellationToken cancellationToken = default)
    {
        var response = new Response<bool>();

        var discount = mapper.Map<Discount>(discountDTO);
        await unitOfWork.Discounts.InsertAsync(discount);
        response.Data = await unitOfWork.Save(cancellationToken) > 0;

        if (response.Data)
        {
            response.IsSuccess = true;
            response.Message = "Registro exitoso.";

            //Creación del evento
            var discountCreatedEvent = mapper.Map<DiscountCreatedEvent>(discount);
            eventBus.Publish(discountCreatedEvent);
        }

        return response;
    }

    public async Task<Response<bool>> Update(DiscountDTO discountDTO, CancellationToken cancellationToken = default)
    {
        var response = new Response<bool>();

        var discount = mapper.Map<Discount>(discountDTO);
        await unitOfWork.Discounts.UpdateAsync(discount);
        response.Data = await unitOfWork.Save(cancellationToken) > 0;

        if (response.Data)
        {
            response.IsSuccess = true;
            response.Message = "Actualización exitosa";
        }

        return response;
    }

    public async Task<Response<bool>> Delete(int id, CancellationToken cancellationToken = default)
    {
        var response = new Response<bool>();

        try
        {
            await unitOfWork.Discounts.DeleteAsync(id.ToString());
            response.Data = await unitOfWork.Save(cancellationToken) > 0;

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación exitosa";
            }
        }
        catch( Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<Response<DiscountDTO>> Get(int id, CancellationToken cancellationToken = default)
    {
        var response = new Response<DiscountDTO>();

        try
        {
            var discount = await unitOfWork.Discounts.GetAsync(id, cancellationToken);

            if(discount is null)
            {
                response.IsSuccess = true;
                response.Message = "El descuento no existe";
                return response;
            }

            response.Data = mapper.Map<DiscountDTO>(discount);
            response.IsSuccess = true;
            response.Message = "Consulta existosa";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<Response<List<DiscountDTO>>> GetAll(CancellationToken cancellationToken = default)
    {
        var response = new Response<List<DiscountDTO>>();

        try
        {
            var discounts = await unitOfWork.Discounts.GetAllAsync(cancellationToken);
            response.Data = mapper.Map<List<DiscountDTO>>(discounts);

            if (response.Data is not null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ResponsePagination<IEnumerable<DiscountDTO>>> GetAllWithPagination(int pageNumber, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<DiscountDTO>>();

        try
        {
            var count = await unitOfWork.Discounts.CountAsync();
            var discounts = await unitOfWork.Discounts.GetAllPaginatedAsync(pageNumber, pageSize);

            response.Data = mapper.Map<IEnumerable<DiscountDTO>>(discounts);

            if (response.Data != null)
            {
                response.PageNumber = pageNumber;
                response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                response.TotalCount = count;
                response.IsSuccess = true;
                response.Message = "Operación realizada con éxito.";
            }

        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }
}
