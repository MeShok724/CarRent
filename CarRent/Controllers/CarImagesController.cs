using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarImagesController : ControllerBase
    {
        private readonly ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarImage>>> GetAll()
        {
            return Ok(await _carImageService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarImage>> GetById(int id)
        {
            var image = await _carImageService.GetByIdAsync(id);
            if (image == null)
                return NotFound();

            return Ok(image);
        }

        [HttpPost]
        public async Task<ActionResult<CarImage>> Create(CarImage image)
        {
            var created = await _carImageService.CreateAsync(image);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _carImageService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }

}
