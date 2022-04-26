using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiCoreApp.API.DTOs;
using MultiCoreApp.API.Filters;
using MultiCoreApp.Core.IntService;
using MultiCoreApp.Core.Models;

namespace MultiCoreApp.API.Controllers
{
    [ValidationFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _catService;
        private IMapper _mapper;

        public CategoryController(ICategoryService catService, IMapper mapper)
        {
            _catService = catService;
            _mapper = mapper;
        }

        [HttpGet] //Select işlemleri için api'da kullanılan keyword'dur.
        public async Task<IActionResult> GetAll()
        {
            var cat = await _catService.GetAllAsync();
            //return Ok(cat);
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(cat));
        }
        [ServiceFilter(typeof(CategoryNotFoundFilter))]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cat = await _catService.GetByIdAsync(id);
            return Ok(_mapper.Map<CategoryDto>(cat));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto catDto)
        {
            var newCat = await _catService.AddAsync(_mapper.Map<Category>(catDto));

            return Created(string.Empty, _mapper.Map<CategoryDto>(newCat));
        }

        [HttpPut]
        public IActionResult Update(CategoryDto catDto)
        {
            var cat = _catService.Update(_mapper.Map<Category>(catDto));
            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Remove(Guid id)
        {
            var cat = _catService.GetByIdAsync(id).Result;
            _catService.Remove(cat);
            return NoContent();
        }

        [HttpDelete("{name}")]
        public IActionResult RemoveByName(string name)
        {
            var cat = _catService.Where(s => s.Name == name).Result;
            _catService.RemoveRange(cat);
            return NoContent();
        }

        [HttpGet("{id:guid}/products")]
        public async Task<IActionResult> GetWithProductById(Guid id)
        {
            var cat = await _catService.GetWithProductByIdAsync(id);
            return Ok(_mapper.Map<CategoryWithProductsDto>(cat));
        }
    }
}
