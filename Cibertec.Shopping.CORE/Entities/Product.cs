using Cibertec.Shopping.CORE.Entities;
using System;
using System.Collections.Generic;

namespace Cibertec.Shopping.CORE.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int? Stock { get; set; }

    public decimal? Price { get; set; }

    public int? Discount { get; set; }

    public int? CategoryId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Favorite> Favorite { get; } = new List<Favorite>();

    public virtual ICollection<OrderDetail> OrderDetail { get; } = new List<OrderDetail>();

    public virtual ICollection<ProductDetail> ProductDetail { get; } = new List<ProductDetail>();
}
