namespace Pacagroup.Ecommerce.Infraestructure.Interface;

public interface IUnitOfWork: IDisposable
{
    ICustomersRepository Customers { get; }
    IUserRepository Users { get; }
    ICategoriesRepository Categories { get; }
}
