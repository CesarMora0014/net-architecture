﻿using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Interface.UseCases;

public interface ICategoriesApplication
{
    Task<Response<IEnumerable<CategoryDTO>>> GetAll();
}
