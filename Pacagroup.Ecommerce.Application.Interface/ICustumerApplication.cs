namespace Pacagroup.Ecommerce.Application.Interface;

using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;
using System.Threading.Tasks;

public interface ICustumerApplication
{
    #region Métodos Síncronos
    Response<bool> Insert(CustomerDTO customerDTO);
    Response<bool> Update(CustomerDTO customerDTO);
    Response<bool> Delete(string customerId);
    Response<CustomerDTO> Get(string customerId);
    Response<IEnumerable<CustomerDTO>> GetAll();
    ResponsePagination<IEnumerable<CustomerDTO>> GetAllPaginated(int pageNumber, int pageSize);
    #endregion

    #region Métodos Asíncronos
    Task<Response<bool>> InsertAsync(CustomerDTO customerDTO);
    Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO);
    Task<Response<bool>> DeleteAsync(string customerId);
    Task<Response<CustomerDTO>> GetAsync(string customerId);
    Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync();
    Task<ResponsePagination<IEnumerable<CustomerDTO>>> GetAllPaginatedAsync(int pageNumber, int pageSize);
    #endregion
}
