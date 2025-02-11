namespace Pacagroup.Ecommerce.Application.Main;

using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Transversal.Common;
using System.Threading.Tasks;
using System.Collections.Generic;

public class CustomerApplication: ICustumerApplication
{
    private readonly ICustomersDomain _customerDomain;
    private readonly IMapper _mapper;

    public CustomerApplication(ICustomersDomain customersDomain, IMapper mapper)
    {
        _customerDomain = customersDomain;
        _mapper = mapper;
    }

    public Response<bool> Insert(CustomerDTO customerDTO)
    {
        var response = new Response<bool>();

        try
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            response.Data = _customerDomain.Insert(customer);

            if (response.Data ) 
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

    public Response<bool> Update(CustomerDTO customerDTO)
    {
        var response = new Response<bool>();

        try
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            response.Data = _customerDomain.Update(customer);

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
            response.Data = _customerDomain.Delete(customerId);

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
            var customer = _customerDomain.Get(customerId);
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
            var customer = _customerDomain.GetAll();
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



    public async Task<Response<bool>> InsertAsync(CustomerDTO customerDTO)
    {
        var response = new Response<bool>();

        try
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            response.Data = await _customerDomain.InsertAsync(customer);

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
            response.Data = await _customerDomain.UpdateAsync(customer);

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
            response.Data = await _customerDomain.DeleteAsync(customerId);

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
            var customer = await _customerDomain.GetAsync(customerId);
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
            var customer = await _customerDomain.GetAllAsync();
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

    
    

    

    
}
