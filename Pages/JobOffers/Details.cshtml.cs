using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreApp.Data;
using CoreApp.Models;

namespace WebApp.Pages.JobOffers
{
    public class DetailsModel : PageModel
    {
        private readonly CoreApp.Data.JobOfferContext _context;

        public DetailsModel(CoreApp.Data.JobOfferContext context)
        {
            _context = context;
        }

        public JobOffer JobOffer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JobOffer = await _context.JobOffer.FirstOrDefaultAsync(m => m.id == id);

            if (JobOffer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
