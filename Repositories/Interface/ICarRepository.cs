using FirsAPIproject.Entites;
using System.Linq.Expressions;

namespace FirsAPIproject.Repositories.Interface
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAll(Expression<Func<Car, bool>>? expression = null, params string[] includes);
        Task<Car> GetByIdAsync(int id);
        Task<Car> Create(Car brand);

        void Update(Car brand);
        void Delete(Car brand);
        Task SaveChangesAsync();


    }
}
