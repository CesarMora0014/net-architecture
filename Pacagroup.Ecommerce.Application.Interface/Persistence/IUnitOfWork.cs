namespace Pacagroup.Ecommerce.Application.Interface.Persistence;

public interface IUnitOfWork: IDisposable
{
    ICustomersRepository Customers { get; }
    IUserRepository Users { get; }
    ICategoriesRepository Categories { get; }
    IDiscountsRepository Discounts {  get; }    
    Task<int> Save(CancellationToken cancellationToken);
}
