using System;
using System.Collections.Generic;

namespace PRN221_MeVaBe_Repo.Models;

public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderDetailId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public double? Price { get; set; }

    public virtual OrderDetail OrderDetail { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
