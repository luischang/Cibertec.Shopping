using Cibertec.Shopping.CORE.Entities;

namespace Cibertec.Shopping.CORE.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Delete(int id);
        Task<User> GetById(int id);
        Task<User> GetUserByCredentials(string email, string password);
        Task<bool> Insert(User user);
        Task<bool> GetByEmail(string email);
    }
}