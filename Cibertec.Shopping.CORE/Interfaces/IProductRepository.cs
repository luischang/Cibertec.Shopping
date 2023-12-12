using Cibertec.Shopping.CORE.Entities;

namespace Cibertec.Shopping.CORE.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<bool> Insert(Product product);
        Task<bool> Update(Product product);
    }
}