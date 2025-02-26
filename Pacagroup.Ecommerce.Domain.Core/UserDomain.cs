using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infraestructure.Interface;

namespace Pacagroup.Ecommerce.Domain.Core;

public class UserDomain : IUserDomain
{
    private readonly IUnitOfWork unitOfWork;
    public UserDomain(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public User Authenticate(string username, string password)
    {
        return unitOfWork.Users.Authenticate(username, password);
    }
}
