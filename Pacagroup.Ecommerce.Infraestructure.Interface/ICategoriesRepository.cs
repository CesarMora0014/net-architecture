using Pacagroup.Ecommerce.Domain.Entity;


namespace Pacagroup.Ecommerce.Infraestructure.Interface;

public interface ICategoriesRepository
{
    Task<IEnumerable<Category>> GetAll();
}
