using FirsAPIproject.DAL;
using FirsAPIproject.DTOs.BrandDtos;
using FirsAPIproject.Entites;
using FirsAPIproject.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirsAPIproject.Controllers
{
    namespace FirsAPIproject.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class BrandController : ControllerBase
        {
            private readonly AppDbContext _context;

            private readonly IBrandRepository _repository;

            public BrandController(AppDbContext context, IBrandRepository repository)
            {
                _context = context;
                _repository = repository;
            }


            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var brands = await _repository.GetAll();
                return StatusCode(200, brands);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                if (id <= 0)
                    return BadRequest("Invalid id value");

                var brand = _repository.GetByIdAsync(id);

                if (brand == null)
                    return NotFound();

                return Ok(brand);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromForm]CreateBrandDto create)
            {
                Brand brand = new Brand()
                {
                    Name = create.Name,
                };


              _repository.Create(brand);
             _repository.SaveChangesAsync();
                return Ok();
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, Brand brand)
            {
                if (id <= 0 || brand == null || brand.Id != id)
                    return BadRequest("Invalid id or brand object");

                var existingBrand = _context.Brands.Find(id);
                if (existingBrand == null)
                    return NotFound();

                existingBrand.Name = brand.Name;

                _repository.Update(existingBrand);
                _repository.SaveChangesAsync();
                return Ok();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                if (id <= 0)
                    return BadRequest("Invalid id value");

                var existingBrand = _context.Brands.Find(id);
                if (existingBrand == null)
                    return NotFound();

                _repository.Delete(existingBrand);
                await _repository.SaveChangesAsync();
                return Ok();
            }
        }
    }
}