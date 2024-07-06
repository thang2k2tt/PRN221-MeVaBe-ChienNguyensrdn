using System;
using System.Collections.Generic;

namespace PRN221_MeVaBe_Repo.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Product> TblProducts { get; set; } = new List<Product>();
}
