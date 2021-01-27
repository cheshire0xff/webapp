using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApp.Data;
using WebApp.Models;
using System.Security.Cryptography;

namespace WebApp.Pages.JobApplications
{

    public class CreateModel : PageModel
    {
        private readonly WebApp.Data.DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(WebApp.Data.DataContext context,
        UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            var offerId = id;
            if (!User.IsInRole("Employer"))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var fId = _context.DatabaseFile.First(df => df.UserId == user.Id).Id;
                JobApplication = new JobApplication{
                    userId = user.Id,
                    fileId = fId,
                    jobOfferId = offerId
                };
            }
            return Page();
        }

        [BindProperty]
        public JobApplication JobApplication { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.JobApplication.Add(JobApplication);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
