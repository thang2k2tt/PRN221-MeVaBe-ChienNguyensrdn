using System;
using System.Collections.Generic;

namespace PRN221_MeVaBe_Repo.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public double? Rate { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? TblProduct { get; set; }

    public virtual User? User { get; set; }
}
