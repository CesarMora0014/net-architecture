namespace Pacagroup.Ecommerce.Application.UseCases;

using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Transversal.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using Pacagroup.Ecommerce.Application.Validator.CustomerDTOValidations;
using Pacagroup.Ecommerce.Application.Interface.UseCases;

public class CustomerApplication: ICustumerApplication
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CustomerApplication> logger;

    public CustomerApplication(IUnitOfWork unitOfWork, IMapper mapper, IAppLogger<CustomerApplication> logger)
    {
        this.unitOfWork = unitOfWork;
        _mapper = mapper;
        this.logger = logger;
    }

    public Response<bool> Insert(CustomerDTO customerDTO)
    {
        var response = new Response<bool>();

        var validation = new InsertCustomerDTOValidator().Validate(customerDTO);

        if (!validation.IsValid)
        {
            response.Message = "Errores de validación";
            response.Errors = validation.Errors;
            return response;
        }


        try
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            response.Data = unitOfWork.Customers.Insert(customer);

            if (response.Data ) 
            {
                response.IsSuccess = true;
                response.Message = "Registro Exitoso.";

                logger.LogInformation("Nuevo registro creado.");
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            response.Message = e.Message;
        }

        return response;
    }

    public Response<bool> Update(CustomerDTO customerDTO)
    {
        var response = new Response<bool>();


        var validation = new UpdateCustomerDTOValidator().Validate(customerDTO);

        if (!validation.IsValid)
        {
            response.Message = "Errores de validación";
            response.Errors = validation.Errors;
            return response;
        }

        try
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            response.Data = unitOfWork.Customers.Update(customer);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Actualización Exitosa.";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }
    
    public Response<bool> Delete(string customerId)
    {
        var response = new Response<bool>();

        try
        {
            response.Data = unitOfWork.Customers.Delete(customerId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación Exitosa.";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

    public Response<CustomerDTO> Get(string customerId)
    {
        var response = new Response<CustomerDTO>();

        try
        {
            var customer = unitOfWork.Customers.Get(customerId);
            response.Data = _mapper.Map<CustomerDTO>(customer);

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa.";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

    public Response<IEnumerable<CustomerDTO>> GetAll()
    {
        var response = new Response<IEnumerable<CustomerDTO>>();

        try
        {
            var customer = unitOfWork.Customers.GetAll();
            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customer);

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa.";

                logger.LogInformation("Consulta Exitosa.");
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            response.Message = e.Message;
        }

        return response;
    }

    public ResponsePagination<IEnumerable<CustomerDTO>> GetAllPaginated(int pageNumber, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<CustomerDTO>>();

        try
        {
            var count = unitOfWork.Customers.Count();
            var customers = unitOfWork.Customers.GetAllPaginated(pageNumber, pageSize);

            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

            if (response.Data != null)
            {
                response.PageNumber = pageNumber;
                response.TotalPages = (int) Math.Ceiling(count / (double) pageSize);
                response.TotalCount = count;
                response.IsSuccess = true;
                response.Message = "Operación realizada con éxito.";
            }

        }
        catch(Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }


    public async Task<Response<bool>> InsertAsync(CustomerDTO customerDTO)
    {
        var response = new Response<bool>();

        try
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            response.Data = await unitOfWork.Customers.InsertAsync(customer);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registro Exitoso.";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }
    
    public async Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO)
    {
        var response = new Response<bool>();

        try
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            response.Data = await unitOfWork.Customers.UpdateAsync(customer);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Actualización Exitosa.";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(string customerId)
    {
        var response = new Response<bool>();

        try
        {
            response.Data = await unitOfWork.Customers.DeleteAsync(customerId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación Exitosa.";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<Response<CustomerDTO>> GetAsync(string customerId)
    {
        var response = new Response<CustomerDTO>();

        try
        {
            var customer = await unitOfWork.Customers.GetAsync(customerId);
            response.Data = _mapper.Map<CustomerDTO>(customer);

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa.";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<CustomerDTO>>();

        try
        {
            var customer = await unitOfWork.Customers.GetAllAsync();
            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customer);

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa.";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<ResponsePagination<IEnumerable<CustomerDTO>>> GetAllPaginatedAsync(int pageNumber, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<CustomerDTO>>();

        try
        {
            var count = await unitOfWork.Customers.CountAsync();
            var customers = await unitOfWork.Customers.GetAllPaginatedAsync(pageNumber, pageSize);

            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

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
