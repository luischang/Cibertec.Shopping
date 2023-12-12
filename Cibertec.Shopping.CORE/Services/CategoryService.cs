using Cibertec.Shopping.CORE.DTOs;
using Cibertec.Shopping.CORE.Entities;
using Cibertec.Shopping.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Shopping.CORE.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryListDTO>> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            var categoriesDTO = categories.Select(category => new CategoryListDTO
            {
                Id = category.Id,
                Description = category.Description,
            });
            return categoriesDTO;
        }

        public async Task<CategoryProductsDTO> GetById(int categoryId, bool includeProducts = false)
        {
            var category = await _categoryRepository.GetByIdWithProducts(categoryId);

            var products = includeProducts ?
                           category.Product.Select(p => new ProductListDTO
                           {
                               Id = p.Id,
                               Description = p.Description,
                               Discount = p.Discount,
                               ImageUrl = p.ImageUrl,
                               Price = p.Price,
                               Stock = p.Stock,
                           }).ToList() : null;

            var categoryProducts = new CategoryProductsDTO()
            {
                Id = category.Id,
                Description = category.Description,
                Products = products
            };
            return categoryProducts;
        }

        public async Task<bool> Insert(CategoryInsertDTO categoryInsertDTO)
        {
            var category = new Category();
            category.Description = categoryInsertDTO.Description;
            category.IsActive = true;

            var result = await _categoryRepository.Insert(category);
            return result;
        }

        public async Task<bool> Update(CategoryListDTO categoryListDTO)
        {
            var category = await _categoryRepository.GetById(categoryListDTO.Id);
            if (category == null)
                return false;

            category.Description = categoryListDTO.Description;
            var result = await _categoryRepository.Update(category);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
                return false;

            var result = await _categoryRepository.Delete(id);
            return result;
        }
    }
}
