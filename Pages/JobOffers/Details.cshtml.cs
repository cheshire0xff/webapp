using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.JobOffers
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp.Data.DataContext _context;

        public DetailsModel(WebApp.Data.DataContext context)
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

            JobOffer = await _context.JobOffer.FirstOrDefaultAsync(m => m.Id == id);

            if (JobOffer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<ActionResult> OnGetDownloadAsync(int id)
        {
            var file = await _context.DatabaseFile.FindAsync(id);
            if (file != null)
            {
                return File(file.Content, "application/pdf");
            }
            return RedirectToPage("./Index");
        }
    }
}
