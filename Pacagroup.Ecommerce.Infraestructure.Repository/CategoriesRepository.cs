using Dapper;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infraestructure.Data;
using Pacagroup.Ecommerce.Infraestructure.Interface;

namespace Pacagroup.Ecommerce.Infraestructure.Repository;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly DapperContext context;

    public CategoriesRepository(DapperContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        using var connection = context.CreateConnection();
        var query = "Select * From Categories";

        var categories = await connection.QueryAsync<Category>(query, commandType: System.Data.CommandType.Text);

        return categories;
    }
}
