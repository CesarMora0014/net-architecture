using Dapper;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infraestructure.Data;
using Pacagroup.Ecommerce.Infraestructure.Interface;
using System.Data;

namespace Pacagroup.Ecommerce.Infraestructure.Repository;

public class UserRepository : IUserRepository
{
    private DapperContext context;

    public UserRepository(DapperContext context)
    {
        this.context = context;
    }

    public User Authenticate(string username, string password)
    {
        using (var connection = context.CreateConnection())
        {
            string query = "UsersGetByUserAndPassword";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UserName", username);
            parameters.Add("Password", password);

            var user  = connection.QuerySingle<User>(query, param: parameters, commandType: CommandType.StoredProcedure);
            return user;
        }
    }

    public bool Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public User Get(string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public bool Insert(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public bool Update(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }
}
