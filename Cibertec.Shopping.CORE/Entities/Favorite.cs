using System;
using System.Collections.Generic;

namespace Cibertec.Shopping.CORE.Entities;

public partial class Favorite
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
