using FirsAPIproject.DAL;
using FirsAPIproject.Entites;
using FirsAPIproject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FirsAPIproject.Repositories.Implementations
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAll(Expression<Func<Car, bool>>? expression = null, params string[] includes)
        {
            IQueryable<Car> query = _context.Cars;

            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            if (expression is not null)
            {
                query = query.Where(expression);
            }

            return await query.ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Cars.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Car> Create(Car car)
        {
            await _context.Cars.AddAsync(car);
            return car;
        }

        public void Update(Car car)
        {
            _context.Cars.Update(car);
        }

        public void Delete(Car car)
        {
            _context.Cars.Remove(car);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
