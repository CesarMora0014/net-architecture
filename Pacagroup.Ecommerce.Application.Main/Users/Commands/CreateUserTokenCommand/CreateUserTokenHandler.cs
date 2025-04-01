using AutoMapper;
using MediatR;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Transversal.Common;


namespace Pacagroup.Ecommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand;

public class CreateUserTokenHandler : IRequestHandler<CreateUserTokenCommand, Response<UserDTO>>
{
    private IUnitOfWork unitOfWork;
    private IMapper mapper;

    public CreateUserTokenHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Response<UserDTO>> Handle(CreateUserTokenCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<UserDTO>();

        var user = await unitOfWork.Users.Authenticate(request.UserName, request.Password);

        if(user is null)
        {
            response.IsSuccess = false;
            response.Message = "Usuario no existe.";
            return response;
        }

        response.Data = mapper.Map<UserDTO>(user);
        response.IsSuccess = true;
        response.Message = "Autenticación exitosa.";

        return response;
    }
}
