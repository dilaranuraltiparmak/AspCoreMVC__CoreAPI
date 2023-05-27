using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Web.Filters;
using NLayer.Core.Services;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsApiController : Controller
    {
        private readonly ProductApiService _service;
        private readonly CategoryApiService _categoryService;
    

        public ProductsApiController(ProductApiService service, CategoryApiService categoryService)
        {
            _service=service;
            _categoryService=categoryService;
       
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetProductsWithCategoriesAsync());
        }

        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();
        
            ViewBag.categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _service.SaveAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            var categoriesDto = await _categoryService.GetAllAsync();
   
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var products = await _service.GetByIdAsync(id);
            var categoriesDto = await _categoryService.GetAllAsync();
          
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", products.CategoryId);
            return View(products);

        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(productDto);

                return RedirectToAction(nameof(Index));
            }
            var categoriesDto = await _categoryService.GetAllAsync();
         
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }


        public async Task<IActionResult> Remove(int id)
        {
        
            await _service.Removeasync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
