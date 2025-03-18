using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Persistence.Contexts;

namespace Pacagroup.Ecommerce.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext applicationDbContext;
    public ICustomersRepository Customers { get; }
    public IUserRepository Users { get; }
    public ICategoriesRepository Categories { get; }
    public IDiscountsRepository Discounts { get; }

    public UnitOfWork(ICustomersRepository customers,
        IUserRepository users,
        ICategoriesRepository categories,
        IDiscountsRepository discounts,
        ApplicationDbContext applicationDbContext)
    {
        Customers = customers;
        Users = users;
        Categories = categories;
        Discounts = discounts;
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<int> Save(CancellationToken cancellationToken)
    {
        return await applicationDbContext.SaveChangesAsync(cancellationToken);
    }


    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    
}
