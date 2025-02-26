using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infraestructure.Interface;
using Dapper;
using System.Data;
using Pacagroup.Ecommerce.Infraestructure.Data;

namespace Pacagroup.Ecommerce.Infraestructure.Repository;

public class CustomerRepository: ICustomersRepository
{
    private readonly DapperContext context;
    public CustomerRepository(DapperContext context)
    {
        this.context = context;
    }

    #region Métodos Síncronos
    public bool Insert(Customer customer)
    {
        using(var connection = context.CreateConnection())
        {
            var query = "CustomersInsert";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customer.CustomerID);
            parameters.Add("CompanyName", customer.CompanyName);
            parameters.Add("ContactName", customer.ContactName);
            parameters.Add("ContactTitle", customer.ContactTitle);
            parameters.Add("Address", customer.Address);
            parameters.Add("City", customer.City);
            parameters.Add("Region", customer.Region);
            parameters.Add("PostalCode", customer.PostalCode);
            parameters.Add("Country", customer.Country);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Fax", customer.Fax);

            var result = connection.Execute(query, param:parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }

    public bool Update(Customer customer)
    {
        using (var connection = context.CreateConnection())
        {
            var query = "CustomersUpdate";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customer.CustomerID);
            parameters.Add("CompanyName", customer.CompanyName);
            parameters.Add("ContactName", customer.ContactName);
            parameters.Add("ContactTitle", customer.ContactTitle);
            parameters.Add("Address", customer.Address);
            parameters.Add("City", customer.City);
            parameters.Add("Region", customer.Region);
            parameters.Add("PostalCode", customer.PostalCode);
            parameters.Add("Country", customer.Country);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Fax", customer.Fax);

            var result = connection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }
    
    public bool Delete(string customerId)
    {
        using (var connection = context.CreateConnection())
        {
            var query = "CustomersDelete";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);

            var result = connection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }

    public Customer Get(string customerId)
    {
        using (var connection = context.CreateConnection())
        {
            var query = "CustomersGetById";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);

            var customer = connection.QuerySingle<Customer>(query, param: parameters, commandType: CommandType.StoredProcedure);

            return customer;
        }
    }

    public IEnumerable<Customer> GetAll()
    {
        using (var connection = context.CreateConnection())
        {
            var query = "CustomersList";
            var customers = connection.Query<Customer>(query, commandType: CommandType.StoredProcedure);

            return customers;
        }
    }

    #endregion

    #region Métodos Asíncronos

    public async Task<bool> InsertAsync(Customer customer)
    {
        using (var connection = context.CreateConnection())
        {
            var query = "CustomersInsert";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customer.CustomerID);
            parameters.Add("CompanyName", customer.CompanyName);
            parameters.Add("ContactName", customer.ContactName);
            parameters.Add("ContactTitle", customer.ContactTitle);
            parameters.Add("Address", customer.Address);
            parameters.Add("City", customer.City);
            parameters.Add("Region", customer.Region);
            parameters.Add("PostalCode", customer.PostalCode);
            parameters.Add("Country", customer.Country);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Fax", customer.Fax);

            var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        using (var connection = context.CreateConnection())
        {
            var query = "CustomersUpdate";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customer.CustomerID);
            parameters.Add("CompanyName", customer.CompanyName);
            parameters.Add("ContactName", customer.ContactName);
            parameters.Add("ContactTitle", customer.ContactTitle);
            parameters.Add("Address", customer.Address);
            parameters.Add("City", customer.City);
            parameters.Add("Region", customer.Region);
            parameters.Add("PostalCode", customer.PostalCode);
            parameters.Add("Country", customer.Country);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Fax", customer.Fax);

            var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }

    public async Task<bool> DeleteAsync(string customerId)
    {
        using (var connection = context.CreateConnection())
        {
            var query = "CustomersDelete";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);

            var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }

    public async Task<Customer> GetAsync(string customerId)
    {
        using (var connection = context.CreateConnection())
        {
            var query = "CustomersGetById";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);

            var customer = await connection.QuerySingleAsync<Customer>(query, param: parameters, commandType: CommandType.StoredProcedure);

            return customer;
        }
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        using (var connection = context.CreateConnection())
        {
            var query = "CustomersList";
            var customers = await connection.QueryAsync<Customer>(query, commandType: CommandType.StoredProcedure);

            return customers;
        }
    }

    #endregion
}
