using System;
using System.Collections.Generic;

namespace PRN221_MeVaBe_Repo.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int PaymentId { get; set; }

    public double? Total { get; set; }

    public string? OrderSatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int UserId { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<OrderItem> TblOrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Payment> TblPayments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
