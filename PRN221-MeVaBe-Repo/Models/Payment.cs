using System;
using System.Collections.Generic;

namespace PRN221_MeVaBe_Repo.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string? TypePayment { get; set; }

    public double? Total { get; set; }

    public int OrderDetailId { get; set; }

    public bool? Status { get; set; }

    public virtual OrderDetail OrderDetail { get; set; } = null!;
}
