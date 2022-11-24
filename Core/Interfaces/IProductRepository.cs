using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProuctByIdAsync(int Id);
        Task<IReadOnlyList<Product>> GetAllProucts();
    }
}
