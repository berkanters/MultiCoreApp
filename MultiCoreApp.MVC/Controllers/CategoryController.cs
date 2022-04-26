using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MultiCoreApp.Core.IntService;
using MultiCoreApp.MVC.ApiServices;
using MultiCoreApp.MVC.DTOs;

namespace MultiCoreApp.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        private readonly CategoryApiService _categoryApiService;
        private IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper, CategoryApiService categoryApiService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _categoryApiService = categoryApiService;
        }
        public async Task<IActionResult> Index()
        {
            //var categories = await _categoryService.GetAllAsync();

            
            IEnumerable<CategoryDto> cat = await _categoryApiService.GetAllAsync();

            //return View(_mapper.Map<IEnumerable<CategoryDto>>(categories));
            return View(cat);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var catDto = await _categoryApiService.GetByIdAsync(id);
            return View(catDto);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            await _categoryApiService.AddAsync(categoryDto);
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Updatee(Guid id)
        {
            var cat=await _categoryApiService.GetByIdAsync(id);
            return View(cat);
        }        
        
        [HttpPost]
        public async Task<IActionResult> Updatee(CategoryDto catDto)
        {
            var cat=await _categoryApiService.Update(catDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var cat = await _categoryApiService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
