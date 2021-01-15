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
    public class DeleteModel : PageModel
    {
        private readonly CoreApp.Data.JobOfferContext _context;

        public DeleteModel(CoreApp.Data.JobOfferContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JobOffer = await _context.JobOffer.FindAsync(id);

            if (JobOffer != null)
            {
                _context.JobOffer.Remove(JobOffer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
