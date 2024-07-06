using System;
using System.Collections.Generic;

namespace PRN221_MeVaBe_Repo.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public double? Total { get; set; }

    public virtual ICollection<CartItem> TblCartItems { get; set; } = new List<CartItem>();

    public virtual User User { get; set; } = null!;
}
