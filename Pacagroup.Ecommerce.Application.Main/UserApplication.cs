using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Validator;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Transversal.Common;
using FluentValidation.Results;

namespace Pacagroup.Ecommerce.Application.Main;

public class UserApplication : IUserApplication
{
    private IUserDomain userDomain;
    private IMapper mapper;
    private readonly UsersDTOValidator validator;

    public UserApplication(IMapper mapper, IUserDomain userDomain, UsersDTOValidator validator)
    {
        this.userDomain = userDomain;
        this.mapper = mapper;
        this.validator = validator; 
    }

    public Response<UserDTO> Authenticate(string username, string password)
    {
        var response = new Response<UserDTO>();

        ValidationResult validation = validator.Validate(new UserDTO { UserName = username, Password = password });

        if(!validation.IsValid)
        {
            response.Message = "Errores de validación";
            response.Errors = validation.Errors;
            return response;
        }

        try
        {
            var user = userDomain.Authenticate(username, password);
            response.Data = mapper.Map<UserDTO>(user);
            response.IsSuccess = true;
            response.Message = "Autenticación exitosa.";
        }
        catch(InvalidOperationException)
        {
            response.IsSuccess = false;
            response.Message = "Usuario no existe.";
        }
        catch(Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
