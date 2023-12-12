using Cibertec.Shopping.CORE.DTOs;
using Cibertec.Shopping.CORE.Entities;
using Cibertec.Shopping.CORE.Interfaces;

namespace Cibertec.Shopping.CORE.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductCategoryDTO>> GetAll()
        {
            var products = await _repository.GetAll();

            var productsDTO = products.Select(p => new ProductCategoryDTO
            {
                Id = p.Id,
                Description = p.Description,
                Discount = p.Discount,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Stock = p.Stock,
                Category = new CategoryListDTO()
                {
                    Id = p.Category.Id,
                    Description = p.Category.Description
                }
            });

            return productsDTO;
        }

        public async Task<ProductCategoryDTO> GetById(int id)
        {
            var product = await _repository.GetById(id);

            var productDTO = new ProductCategoryDTO()
            {
                Id = product.Id,
                Description = product.Description,
                Discount = product.Discount,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Stock = product.Stock,
                Category = new CategoryListDTO()
                {
                    Id = product.Category.Id,
                    Description = product.Category.Description
                }
            };
            return productDTO;
        }

        public async Task<bool> Insert(ProductInsertDTO productInsertDTO)
        {
            var product = new Product()
            {
                Description = productInsertDTO.Description,
                ImageUrl = productInsertDTO.ImageUrl,
                Price = productInsertDTO.Price,
                Stock = productInsertDTO.Stock,
                Discount= productInsertDTO.Discount,
                IsActive = true,
                CategoryId = productInsertDTO.CategoryId
            };
            return await _repository.Insert(product);
        }

        public async Task<bool> Update(ProductUpdateDTO productUpdateDTO)
        {
            var product = await _repository.GetById(productUpdateDTO.Id);
            if (product == null)
                return false;

            product.Description = productUpdateDTO.Description;
            product.ImageUrl = productUpdateDTO.ImageUrl;
            product.Price = productUpdateDTO.Price;
            product.Stock = productUpdateDTO.Stock;
            product.Discount = productUpdateDTO.Discount;
            product.CategoryId = productUpdateDTO.CategoryId;    

            return await _repository.Update(product);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }


    }
}
