using Cibertec.Shopping.CORE.DTOs;

namespace Cibertec.Shopping.CORE.Interfaces
{
    public interface IProductService
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<ProductCategoryDTO>> GetAll();
        Task<ProductCategoryDTO> GetById(int id);
        Task<bool> Insert(ProductInsertDTO productInsertDTO);
        Task<bool> Update(ProductUpdateDTO productUpdateDTO);
    }
}