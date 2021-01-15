using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreApp.Data;
using CoreApp.Models;

namespace WebApp.Pages.JobOffers
{
    [Authorize(Roles = "Administrator,Employer")]
    public class CreateModel : PageModel
    {
        private readonly CoreApp.Data.JobOfferContext _context;

        public CreateModel(CoreApp.Data.JobOfferContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public JobOffer JobOffer { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.JobOffer.Add(JobOffer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
