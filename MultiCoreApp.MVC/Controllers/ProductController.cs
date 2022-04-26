using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiCoreApp.Core.IntService;
using MultiCoreApp.MVC.ApiServices;
using MultiCoreApp.MVC.DTOs;
using MultiCoreApp.Service.Services;

namespace MultiCoreApp.MVC.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ProductApiService _productApiService;
        private IMapper _mapper;
        private CategoryApiService _categoryApiService;

        public ProductController(IProductService productService, ProductApiService productApiService, IMapper mapper, CategoryApiService categoryApiService)
        {
            _productService = productService;
            _productApiService = productApiService;
            _mapper = mapper;
            _categoryApiService = categoryApiService;
        }
        public async Task<IActionResult> Index()
        {
            //var pro = await _productService.GetAllAsync();
            IEnumerable<ProductWithCategoryDto> pro = await _productApiService.GetAllAsyncWithCategory();
            return View(pro);
            //return View(_mapper.Map<IEnumerable<CategoryDto>>(pro));
        }

        public async Task<IActionResult> Detailse(Guid id)
        {
            ProductWithCategoryDto pro = await _productApiService.GetByIdAsync(id);
            return View(pro);
        }

        public IActionResult Create()
        {
            var cat = _categoryApiService.GetAllAsync().Result;
            ViewData["CategoryId"] = new SelectList(cat, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductWithCategoryDto proDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.AddAsync(proDto);
                RedirectToAction("Index"); 
            }

            
            ViewData["CategoryId"] = new SelectList(_categoryApiService.GetAllAsync().Result, "Id", "Name",
                proDto.CategoryId);
            return View(proDto);
        }

        public async Task<IActionResult> Updatee(Guid id)
        {

            var proDto = await _productApiService.GetByIdAsync(id);
            ViewData["CategoryId"] = new SelectList(_categoryApiService.GetAllAsync().Result, "Id", "Name",
                proDto.CategoryId);


            return View(proDto);
        }

        [HttpPost]
        public async Task<IActionResult> Updatee(ProductWithCategoryDto productDto)
        {
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                await _productApiService.Update(productDto);
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_categoryApiService.GetAllAsync().Result, "Id", "Name",
                productDto.CategoryId);
            return View(productDto);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            ProductWithCategoryDto pro = await _productApiService.GetByIdWithCategryAsync(id);
            return View(pro);
        }
    }
}
