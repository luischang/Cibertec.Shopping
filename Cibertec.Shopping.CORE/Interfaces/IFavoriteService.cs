using Cibertec.Shopping.CORE.DTOs;

namespace Cibertec.Shopping.CORE.Interfaces
{
    public interface IFavoriteService
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<FavoriteListDTO>> GetAll(int userId);
        Task<bool> Insert(FavoriteInsertDTO favoriteInsertDTO);
    }
}