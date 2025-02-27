namespace Pacagroup.Ecommerce.Domain.Core;

using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infraestructure.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;

public class CustomersDomain: ICustomersDomain
{
    private readonly IUnitOfWork unitOfWork;

    public CustomersDomain(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    #region Métodos Síncronos
    public bool Insert(Customer customer)
    {
        return unitOfWork.Customers.Insert(customer);
    }

    public bool Update(Customer customer)
    {
        return unitOfWork.Customers.Update(customer);
    }

    public bool Delete(string customerId)
    {
        return unitOfWork.Customers.Delete(customerId);
    }

    public Customer Get(string customerId)
    {
        return unitOfWork.Customers.Get(customerId);
    }
    public IEnumerable<Customer> GetAll()
    {
        return unitOfWork.Customers.GetAll();
    }

    public IEnumerable<Customer> GetAllPaginated(int pageNumber, int pageSize)
    {
        return unitOfWork.Customers.GetAllPaginated(pageNumber, pageSize);
    }

    public int Count()
    {
        return unitOfWork.Customers.Count();
    }

    #endregion

    #region Métodos Asíncronos
    public async Task<bool> InsertAsync(Customer customer)
    {
        return await unitOfWork.Customers.InsertAsync(customer);
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        return await unitOfWork.Customers.UpdateAsync(customer);
    }

    public async Task<bool> DeleteAsync(string customerId)
    {
        return await unitOfWork.Customers.DeleteAsync(customerId);
    }

    public async Task<Customer> GetAsync(string customerId)
    {
        return await unitOfWork.Customers.GetAsync(customerId);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await unitOfWork.Customers.GetAllAsync();
    }

    public async Task<IEnumerable<Customer>> GetAllPaginatedAsync(int pageNumber, int pageSize)
    {
        return await unitOfWork.Customers.GetAllPaginatedAsync(pageNumber, pageSize);
    }

    public async Task<int> CountAsync()
    {
        return await unitOfWork.Customers.CountAsync();
    }

    #endregion
}
