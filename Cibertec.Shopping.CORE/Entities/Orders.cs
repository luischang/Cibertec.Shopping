using System;
using System.Collections.Generic;

namespace Cibertec.Shopping.CORE.Entities;

public partial class Orders
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UserId { get; set; }

    public string? Status { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual ICollection<OrderDetail> OrderDetail { get; } = new List<OrderDetail>();

    public virtual ICollection<Payment> Payment { get; } = new List<Payment>();

    public virtual User? User { get; set; }
}
