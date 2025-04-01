using Dapper;
using Pacagroup.Ecommerce.Persistence.Contexts;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using System.Data;
using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Persistence.Repositories;

public class UsersRepository : IUserRepository
{
    private DapperContext context;

    public UsersRepository(DapperContext context)
    {
        this.context = context;
    }

    public async Task<User> Authenticate(string username, string password)
    {
        using (var connection = context.CreateConnection())
        {
            string query = "UsersGetByUserAndPassword";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UserName", username);
            parameters.Add("Password", password);

            var user = await connection.QuerySingleOrDefaultAsync<User>(query, param: parameters, commandType: CommandType.StoredProcedure);
            return user;
        }
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
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

    public IEnumerable<User> GetAllPaginated(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllPaginatedAsync(int pageNumber, int pageSize)
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
