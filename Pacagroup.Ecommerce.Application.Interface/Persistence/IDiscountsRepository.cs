using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Application.Interface.Persistence;

public interface IDiscountsRepository : IGenericRepository<Discount>
{
    Task<Discount> GetAsync(int id, CancellationToken cancellationToken);
    Task<List<Discount>> GetAllAsync(CancellationToken cancellationToken);
}
