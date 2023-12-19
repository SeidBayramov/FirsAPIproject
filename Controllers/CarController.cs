using FirsAPIproject.DAL;
using FirsAPIproject.Entites;
using FirsAPIproject.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirsAPIproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICarRepository _carRepository;

        public CarController(AppDbContext context, ICarRepository carRepository)
        {
            _context = context;
            _carRepository = carRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _carRepository.GetAll();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid id value");

            var car = await _carRepository.GetByIdAsync(id);

            if (car == null)
                return NotFound();

            return Ok(car);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] Car car)
        {
            if (car == null || car.Id != 0)
                return BadRequest("Invalid car object");

            await _carRepository.Create(car);
            await _carRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] Car car)
        {
            if (id <= 0 || car == null || car.Id != id)
                return BadRequest("Invalid id or car object");

            var existingCar = await _carRepository.GetByIdAsync(id);
            if (existingCar == null)
                return NotFound();

            existingCar.BrandId = car.BrandId;
            existingCar.ColorId = car.ColorId;
            existingCar.ModelYear = car.ModelYear;
            existingCar.DailyPrice = car.DailyPrice;
            existingCar.Description = car.Description;

            _carRepository.Update(existingCar);
            await _carRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid id value");

            var existingCar = await _carRepository.GetByIdAsync(id);
            if (existingCar == null)
                return NotFound();

            _carRepository.Delete(existingCar);
            await _carRepository.SaveChangesAsync();

            return Ok();
        }
    }
}
