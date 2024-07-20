﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_MeVaBe_Repo.Models;

namespace PRN221_MeVaBe_WebClient_Adminn.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly PRN221_MeVaBe_Repo.Models.DBContext _context;

        public DetailsModel(PRN221_MeVaBe_Repo.Models.DBContext context)
        {
            _context = context;
        }

        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.TblOrderDetails.FirstOrDefaultAsync(m => m.Id == id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            else
            {
                OrderDetail = orderdetail;
            }
            return Page();
        }
    }
}