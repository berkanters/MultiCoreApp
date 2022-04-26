using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiCoreApp.API.DTOs;
using MultiCoreApp.API.Filters;
using MultiCoreApp.Core.IntService;
using MultiCoreApp.Core.Models;

namespace MultiCoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _proService;
        private IMapper _mapper;

        public ProductController(IProductService proService, IMapper mapper)
        {
            _proService = proService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pro = await _proService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(pro));
        }

        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var pro= await _proService.GetByIdAsync(id);
            return Ok(_mapper.Map<ProductDto>(pro));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto proDto)
        {
            var pro = await _proService.AddAsync(_mapper.Map<Product>(proDto));
            return Created(string.Empty, _mapper.Map<ProductDto>(pro));
        }

        [HttpPut]
        public IActionResult Update(ProductDto proDto)
        {
            var pro = _proService.Update(_mapper.Map<Product>(proDto));
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Remove(Guid id)
        {
            var pro = _proService.GetByIdAsync(id).Result;
            _proService.Remove(pro);
            return NoContent();
        }

        [HttpDelete("{name}")]
        public IActionResult RemoveByName(string name)
        {
            var pro = _proService.Where(s => s.Name == name).Result;
            _proService.RemoveRange(pro);
            return NoContent();
        }
        [HttpGet("{id:guid}/category")]
        public async Task<IActionResult> GetWithCategoryById(Guid id)
        {
            var pro = await _proService.GetWithCategoryByIdAsync(id);
            return Ok(_mapper.Map<ProductWithCategoryDto>(pro));
        }

        [HttpGet("categoryall")]
        public async Task<IActionResult> GetAllWithCategoryAsync()
        {
            var pro = await _proService.GetAllWithCategoryAsync();
            return Ok(_mapper.Map<IEnumerable<ProductWithCategoryDto>>(pro));
        }


    }
}
