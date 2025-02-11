namespace Pacagroup.Ecommerce.Infraestructure.Data;

using Pacagroup.Ecommerce.Transversal.Common;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
public class ConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration configuration;

    public ConnectionFactory(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IDbConnection GetConnection
    {
        get
        {
            var sqlConnection = new SqlConnection();

            if (sqlConnection == null) return null;

            sqlConnection.ConnectionString = configuration.GetConnectionString("NorthwindConnection");
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}
