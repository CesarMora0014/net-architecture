using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Transversal.Common;


namespace Pacagroup.Ecommerce.Application.Main;

public class CategoriesApplication : ICategoriesApplication
{
    private readonly ICategoriesDomain categoriesDomain;
    private readonly IMapper mapper;

    public CategoriesApplication(ICategoriesDomain categoriesDomain, IMapper mapper)
    {
        this.categoriesDomain = categoriesDomain;
        this.mapper = mapper;
    }

    public async Task<Response<IEnumerable<CategoryDTO>>> GetAll()
    {
        var response = new Response<IEnumerable<CategoryDTO>>();

        try
        {
            var categories = await categoriesDomain.GetAll(); 
            response.Data = mapper.Map<IEnumerable<CategoryDTO>>(categories);

            if(response.Data is not null)
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
