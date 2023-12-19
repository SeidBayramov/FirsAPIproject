using FirsAPIproject.Entites;
using System.Linq.Expressions;

namespace FirsAPIproject.Repositories.Interface
{
    public interface IBrandRepository
    {

        Task<IEnumerable<Brand>> GetAll(Expression<Func<Brand, bool>>? expression1 = null, params string[] includes);
        Task<Brand> GetByIdAsync(int id);
        Task<Brand> Create(Brand brand);

        void Update(Brand brand );
        void Delete(Brand brand);
        Task SaveChangesAsync();
        

    }
}
