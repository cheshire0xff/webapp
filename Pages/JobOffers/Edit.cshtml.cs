using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using Microsoft.AspNetCore.Identity;


namespace WebApp.Pages.JobOffers
{
    public class EditModel : PageModel
    {
        private readonly WebApp.Data.DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(WebApp.Data.DataContext context,
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(JobOffer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobOfferExists(JobOffer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool JobOfferExists(int id)
        {
            return _context.JobOffer.Any(e => e.Id == id);
        }
    }
}
