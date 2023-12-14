using Cibertec.Shopping.CORE.Entities;

namespace Cibertec.Shopping.CORE.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<Favorite>> GetAll(int userId);
        Task<bool> Insert(Favorite favorite);
    }
}