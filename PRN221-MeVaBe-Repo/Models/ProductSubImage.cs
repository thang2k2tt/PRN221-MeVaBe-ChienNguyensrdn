using System;
using System.Collections.Generic;

namespace PRN221_MeVaBe_Repo.Models;

public partial class ProductSubImage
{
    public int Id { get; set; }

    public string? Url { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
