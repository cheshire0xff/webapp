using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.JobOffers
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(WebApp.Data.DataContext context,
        UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IList<JobOffer> JobOffer { get;set; }
        public JobOffer JobOfferSingle { get; set; }

        public async Task<ActionResult> OnGetDownloadAsync(int id)
        {
            var file = await _context.DatabaseFile.FindAsync(id);
            if (file != null)
            {
                return File(file.Content, "application/pdf");
            }
            return RedirectToPage("./Index");
        }


        public async Task OnGetAsync(int jobId)
        {
            JobOffer = await _context.JobOffer.ToListAsync();



            if (User.IsInRole("Employer"))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                JobOffer = await _context.JobOffer.Where(jo => jo.EmployerId == user.Id).ToListAsync();
            }
            else
            {
                JobOffer = await _context.JobOffer.ToListAsync();
            }
            if (JobOffer.Count() == 0)
            {
                return;
            }
            IQueryable<JobOffer> jobsIQ = from s in JobOffer.AsQueryable()
                                          select s;
            if (jobId == 0)
                jobId = JobOffer[0].Id;
            jobsIQ = jobsIQ.Where(s => s.Id == jobId);
            JobOfferSingle = jobsIQ.AsNoTracking().First();
        }
    }
}
