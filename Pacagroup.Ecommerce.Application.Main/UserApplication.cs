using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Validator;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Transversal.Common;
using FluentValidation.Results;
using Pacagroup.Ecommerce.Application.Interface.UseCases;

namespace Pacagroup.Ecommerce.Application.UseCases;

public class UserApplication : IUserApplication
{
    private IUnitOfWork unitOfWork;
    private IMapper mapper;
    private readonly UsersDTOValidator validator;

    public UserApplication(IMapper mapper, IUnitOfWork unitOfWork, UsersDTOValidator validator)
    {
        this.unitOfWork = unitOfWork;
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
            var user = unitOfWork.Users.Authenticate(username, password);
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
