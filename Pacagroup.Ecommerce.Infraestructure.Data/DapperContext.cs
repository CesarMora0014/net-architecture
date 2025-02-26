using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Pacagroup.Ecommerce.Infraestructure.Data;

public class DapperContext
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;

    public DapperContext(IConfiguration configuration)
    {
        this.configuration = configuration;
        this.connectionString = this.configuration.GetConnectionString("NorthwindConnection");
    }

    public IDbConnection CreateConnection() => new SqlConnection(connectionString);
}
