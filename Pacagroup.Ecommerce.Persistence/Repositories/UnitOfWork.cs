using Pacagroup.Ecommerce.Application.Interface.Persistence;

namespace Pacagroup.Ecommerce.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public ICustomersRepository Customers { get; }
    public IUserRepository Users { get; }
    public ICategoriesRepository Categories { get; }

    public UnitOfWork(ICustomersRepository customers, IUserRepository users, ICategoriesRepository categories)
    {
        Customers = customers;
        Users = users;
        Categories = categories;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
