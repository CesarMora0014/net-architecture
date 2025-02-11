using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Main;

public class UserApplication : IUserApplication
{
    private IUserDomain userDomain;
    private IMapper mapper;

    public UserApplication(IMapper mapper, IUserDomain userDomain)
    {
       this.userDomain = userDomain;
       this.mapper = mapper;
    }

    public Response<UserDTO> Authenticate(string username, string password)
    {
        var response = new Response<UserDTO>();

        if(username == null || password == null)
        {
            response.Message = "Párametros no pueden ser vacíos";
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
            response.IsSuccess = true;
            response.Message = "Usuario no existe.";
        }
        catch(Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
