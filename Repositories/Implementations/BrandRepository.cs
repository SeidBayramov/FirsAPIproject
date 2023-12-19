using FirsAPIproject.DAL;
using FirsAPIproject.Entites;
using FirsAPIproject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FirsAPIproject.Repositories.Implementations
{
    public class BrandRepository : IBrandRepository
    {
        AppDbContext _context;

        public BrandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetAll(Expression<Func<Brand, bool>>? expression = null, params string[] includes)
        {

            IQueryable<Brand> query = _context.Brands;

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


            return query;


        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _context.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }



        public async Task<Brand> Create(Brand brand)
        {
            await _context.Brands.AddAsync(brand);

            return brand;
        }

        public void Update(Brand brand)
        {
            _context.Brands.Update(brand);
        }

        public void Delete(Brand brand)
        {
            _context.Brands.Remove(brand);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }

}
