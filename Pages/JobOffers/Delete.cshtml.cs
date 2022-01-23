using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.JobOffers
{
    public class DeleteModel : PageModel
    {
        private readonly WebApp.Data.DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteModel(WebApp.Data.DataContext context,
        UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public JobOffer JobOffer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (id == null || user == null)
            {
                return NotFound();
            }

            JobOffer = await _context.JobOffer.FirstOrDefaultAsync(m => m.Id == id);
            if (JobOffer == null)
            {
                return NotFound();
            }
            
            if (User == null)
            {
                return NotFound();
            }
            if (user.Id != JobOffer.EmployerId && !User.IsInRole("Administrator"))
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (id == null || user == null || User == null)
            {
                return NotFound();
            }

            JobOffer = await _context.JobOffer.FindAsync(id);
            if (user.Id != JobOffer.EmployerId && !User.IsInRole("Administrator"))
            {
                return NotFound();
            }

            if (JobOffer != null)
            {
                _context.JobOffer.Remove(JobOffer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
