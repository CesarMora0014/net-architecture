using Pacagroup.Ecommerce.Infraestructure.Interface;

namespace Pacagroup.Ecommerce.Infraestructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    public ICustomersRepository Customers { get; }
    public IUserRepository Users { get; }

    public UnitOfWork(ICustomersRepository customers, IUserRepository users)
    {
        Customers = customers;
        Users = users;
    }

    public void Dispose()
    {
        System.GC.SuppressFinalize(this);
    }
}
