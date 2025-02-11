namespace Pacagroup.Ecommerce.Domain.Core;

using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infraestructure.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;

public class CustomersDomain: ICustomersDomain
{
    private readonly ICustomersRepository customersRepository;

    public CustomersDomain(ICustomersRepository customersRepository)
    {
        this.customersRepository = customersRepository;
    }

    #region Métodos Síncronos
    public bool Insert(Customer customer)
    {
        return customersRepository.Insert(customer);
    }

    public bool Update(Customer customer)
    {
        return customersRepository.Update(customer);
    }

    public bool Delete(string customerId)
    {
        return customersRepository.Delete(customerId);
    }

    public Customer Get(string customerId)
    {
        return customersRepository.Get(customerId);
    }
    public IEnumerable<Customer> GetAll()
    {
        return customersRepository.GetAll();
    }

    #endregion

    #region Métodos Asíncronos
    public async Task<bool> InsertAsync(Customer customer)
    {
        return await customersRepository.InsertAsync(customer);
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        return await customersRepository.UpdateAsync(customer);
    }

    public async Task<bool> DeleteAsync(string customerId)
    {
        return await customersRepository.DeleteAsync(customerId);
    }

    public async Task<Customer> GetAsync(string customerId)
    {
        return await customersRepository.GetAsync(customerId);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await customersRepository.GetAllAsync();
    }

    #endregion
}
