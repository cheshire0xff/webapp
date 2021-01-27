using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.JobApplications
{
    public class JobApplicationJobOffer
    {
        public JobApplicationJobOffer(
            JobApplication jobApplication, 
            JobOffer jobOffer)
        {
            JobApplication = jobApplication;
            JobOffer = jobOffer;
        }
        public JobApplication JobApplication {get; set;}
        public JobOffer JobOffer {get; set;}
    }
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
        public IList<JobApplicationJobOffer> JobApplicationJobOffer { get;set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            IList<JobApplication> JobApplication;
            if (User.IsInRole("Administrator"))
            {
                JobApplication = await _context.JobApplication.ToListAsync();
            }
            else if (!User.IsInRole("Employer"))
            {
                JobApplication = await _context.JobApplication.Where(ja => ja.userId == user.Id).ToListAsync();
            }
            else
            {
               List<JobOffer> jobOffers = await _context.JobOffer.Where(jo => jo.employerId == user.Id).ToListAsync();
               JobApplication = Enumerable.Join(
                   _context.JobApplication,
                   jobOffers,
                   ja => ja.jobOfferId,
                   eo => eo.id ,
                   (ja, eo) => ja
               ).ToList();
            }
            JobApplicationJobOffer = Enumerable.Join(
                JobApplication,
                _context.JobOffer,
                ja => ja.jobOfferId,
                jo => jo.id,
                (ja, jo) => new JobApplicationJobOffer(ja, jo)
            ).ToList();
        }
    }
}
