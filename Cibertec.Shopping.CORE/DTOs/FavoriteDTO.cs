using Cibertec.Shopping.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Shopping.CORE.DTOs
{
    public class FavoriteDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }

    public class FavoriteInsertDTO
    {
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
    }

    public class FavoriteListDTO
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ProductCategoryDTO Product { get; set; }
    }

}
