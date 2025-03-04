using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infraestructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Domain.Core;

public class CategoriesDomain : ICategoriesDomain
{
    private readonly IUnitOfWork unitOfWork;

    public CategoriesDomain(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await unitOfWork.Categories.GetAll();
    }
}
