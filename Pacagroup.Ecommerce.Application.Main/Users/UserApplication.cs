using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Transversal.Common;
using FluentValidation.Results;
using Pacagroup.Ecommerce.Application.Interface.UseCases;

namespace Pacagroup.Ecommerce.Application.UseCases.Users;

public class UserApplication : IUserApplication
{
    private IUnitOfWork unitOfWork;
    private IMapper mapper;
   
    public UserApplication(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public Response<UserDTO> Authenticate(string username, string password)
    {
        var response = new Response<UserDTO>();

        var user = unitOfWork.Users.Authenticate(username, password);
        response.Data = mapper.Map<UserDTO>(user);
        response.IsSuccess = true;
        response.Message = "Autenticación exitosa.";

        return response;
    }

}
