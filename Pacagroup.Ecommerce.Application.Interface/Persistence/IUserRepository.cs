using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Application.Interface.Persistence;

public interface IUserRepository: IGenericRepository<User>
{
    Task<User> Authenticate(string username, string password);
}
