using System;
using System.Collections.Generic;

namespace PRN221_MeVaBe_Repo.Models;

public partial class UserAddress
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Province { get; set; }

    public string? District { get; set; }

    public string? Ward { get; set; }

    public string? Address { get; set; }

    public virtual User? User { get; set; }
}
