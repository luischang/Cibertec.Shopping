﻿using System;
using System.Collections.Generic;

namespace Cibertec.Shopping.CORE.Entities;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? OrdersId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Orders? Orders { get; set; }

    public virtual Product? Product { get; set; }
}
