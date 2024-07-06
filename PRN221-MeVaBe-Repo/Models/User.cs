using System;
using System.Collections.Generic;

namespace PRN221_MeVaBe_Repo.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Avatar { get; set; }

    public DateOnly? Dob { get; set; }

    public bool? Status { get; set; }

    public string? Password { get; set; }

    public string? Fullname { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Cart> TblCarts { get; set; } = new List<Cart>();

    public virtual ICollection<Feedback> TblFeedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderDetail> TblOrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<UserAddress> TblUserAddresses { get; set; } = new List<UserAddress>();
}
