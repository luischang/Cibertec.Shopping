using Cibertec.Shopping.CORE.DTOs;

namespace Cibertec.Shopping.CORE.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<CategoryListDTO>> GetAll();
        Task<CategoryProductsDTO> GetById(int categoryId, bool includeProducts = false);
        Task<bool> Insert(CategoryInsertDTO categoryInsertDTO);
        Task<bool> Update(CategoryListDTO categoryListDTO);
    }
}