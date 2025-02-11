using Dapper;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infraestructure.Interface;
using Pacagroup.Ecommerce.Transversal.Common;
using System.Data;

namespace Pacagroup.Ecommerce.Infraestructure.Repository;

public class UserRepository : IUserRepository
{
    private IConnectionFactory connectionFactory;

    public UserRepository(IConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
    }

    public User Authenticate(string username, string password)
    {
        using (var connection = connectionFactory.GetConnection)
        {
            string query = "UsersGetByUserAndPassword";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UserName", username);
            parameters.Add("Password", password);

            var user  = connection.QuerySingle<User>(query, param: parameters, commandType: CommandType.StoredProcedure);
            return user;
        }
    }
}
