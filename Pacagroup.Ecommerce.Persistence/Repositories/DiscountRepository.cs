using Bogus;
using Microsoft.EntityFrameworkCore;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Persistence.Contexts;
using Pacagroup.Ecommerce.Persistence.Mocks;

namespace Pacagroup.Ecommerce.Persistence.Repositories;

public class DiscountRepository : IDiscountsRepository
{
    protected readonly ApplicationDbContext applicationDbContext;

    public DiscountRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public int Count()
    {
        throw new NotImplementedException();
    }
    public bool Update(Discount entity)
    {
        throw new NotImplementedException();
    }
    public bool Insert(Discount entity)
    {
        throw new NotImplementedException();
    }
    public IEnumerable<Discount> GetAllPaginated(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
    public IEnumerable<Discount> GetAll()
    {
        throw new NotImplementedException();
    }
    public Discount Get(string id)
    {
        throw new NotImplementedException();
    }
    public bool Delete(string id)
    {
        throw new NotImplementedException();
    }


    public async Task<bool> InsertAsync(Discount discount)
    {
        await applicationDbContext.AddAsync(discount);
        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateAsync(Discount discount)
    {
        var entity = await applicationDbContext.Set<Discount>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals(discount.Id));

        if (entity is null)
        {
            return await Task.FromResult(false);
        }

        entity.Name = discount.Name;
        entity.Description = discount.Description;
        entity.Percent = discount.Percent;
        entity.Status = discount.Status;

        applicationDbContext.Update(entity);

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var entity = await applicationDbContext.Set<Discount>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals(int.Parse(id)));

        if(entity is null)
        {
            return await Task.FromResult(false);
        }

        applicationDbContext.Remove(entity);

        return await Task.FromResult(true);
    }

    public async Task<List<Discount>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await applicationDbContext.Set<Discount>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Discount> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await applicationDbContext.Set<Discount>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public async Task<int> CountAsync()
    {
        return await Task.Run(() => 1000);
    }    

    public Task<IEnumerable<Discount>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Discount>> GetAllPaginatedAsync(int pageNumber, int pageSize)
    {
        var faker = new DiscountGetAllWithPaginationAsyncBogusConfig();
        var result = await Task.Run(() => faker.Generate(1000));

        return result.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }

    public Task<Discount> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    
}
