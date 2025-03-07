using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Transversal.Common;
using System.Text;
using System.Text.Json;
using Pacagroup.Ecommerce.Application.Interface.Persistence;


namespace Pacagroup.Ecommerce.Application.UseCases.Categories;

public class CategoriesApplication : ICategoriesApplication
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IDistributedCache distributedCache;

    public CategoriesApplication(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache distributedCache)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.distributedCache = distributedCache;
    }

    public async Task<Response<IEnumerable<CategoryDTO>>> GetAll()
    {
        var response = new Response<IEnumerable<CategoryDTO>>();
        var cacheKey = "categoriesList";

        try
        {
            var redisCategories = await distributedCache.GetAsync(cacheKey);

            if (redisCategories is not null)
            {
                response.Data = JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(redisCategories);
            }
            else
            {
                var categories = await unitOfWork.Categories.GetAll();
                response.Data = mapper.Map<IEnumerable<CategoryDTO>>(categories);

                if (response.Data is not null)
                {
                    var serializedCategories = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response.Data));

                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddHours(8))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(60));

                    await distributedCache.SetAsync(cacheKey, serializedCategories, options: options);
                }
            }

            if (response.Data is not null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
            }


        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
