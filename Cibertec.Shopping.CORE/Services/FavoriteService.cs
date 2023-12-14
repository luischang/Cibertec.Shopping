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
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await _favoriteRepository.Delete(id);
        }

        public async Task<bool> Insert(FavoriteInsertDTO favoriteInsertDTO)
        {
            var favorite = new Favorite()
            {
                UserId = favoriteInsertDTO.UserId,
                ProductId = favoriteInsertDTO.ProductId,
                CreatedAt = DateTime.UtcNow,
            };

            return await _favoriteRepository.Insert(favorite);
        }

        public async Task<IEnumerable<FavoriteListDTO>> GetAll(int userId)
        {
            var favorites = await _favoriteRepository.GetAll(userId);

            var favoritesDTO = favorites.Select(favorite => new FavoriteListDTO
            {
                Id = favorite.Id,
                CreatedAt = favorite.CreatedAt,
                Product = new ProductCategoryDTO
                {
                    Id = favorite.Product.Id,
                    Description = favorite.Product.Description,
                    Price = favorite.Product.Price,
                    Stock = favorite.Product.Stock,
                    Discount = favorite.Product.Discount,
                    ImageUrl = favorite.Product.ImageUrl,
                    Category = new CategoryListDTO
                    {
                        Id = favorite.Product.Category.Id,
                        Description = favorite.Product.Category.Description
                    }
                }
            }).ToList();

            return favoritesDTO;

        }



    }
}
