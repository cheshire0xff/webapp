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
        public IList<JobOffer> JobOffer { get; set; }
        public JobOffer JobOfferSingle { get; set; }
        public string tagsSort { get; set; }
        public string localizationSort { get; set; }
        public string descriptionSort { get; set; }
        public string addedDateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task<ActionResult> OnGetDownloadAsync(int id)
        {
            var file = await _context.DatabaseFile.FindAsync(id);
            if (file != null)
            {
                return File(file.Content, "application/pdf");
            }
            return RedirectToPage("./Index");
        }


        public async Task OnGetAsync(int jobId, string sortOrder, string searchString)
        {
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
            descriptionSort = 
                string.IsNullOrEmpty(sortOrder) || sortOrder == "description_desc" ? "description_asc" : "description_desc";
            localizationSort = sortOrder == "localization_asc" ? "localization_desc" : "localization_asc";
            tagsSort = sortOrder == "tags_asc" ? "tags_desc" : "tags_asc";
            addedDateSort = sortOrder == "expirationDate_asc" ? "expirationDate_desc" : "expirationDate_asc";
            CurrentSort = sortOrder; 
            CurrentFilter = searchString;

            IQueryable<JobOffer> jobsIQ = from s in JobOffer.AsQueryable()
                                          select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                jobsIQ = jobsIQ.Where(s => s.Description.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));
            }

            switch (sortOrder)
            {
                case "description_desc":
                    jobsIQ = jobsIQ.OrderByDescending(s => s.Description);
                    descriptionSort = "description_asc";
                    break;
                case "description_asc":
                    jobsIQ = jobsIQ.OrderBy(s => s.Description);
                    descriptionSort = "description_desc";
                    break;
                case "localization_desc":
                    jobsIQ = jobsIQ.OrderByDescending(s => s.Localization);
                    localizationSort = "localization_asc";
                    break;
                case "localization_asc":
                    jobsIQ = jobsIQ.OrderBy(s => s.Localization);
                    localizationSort = "localization_desc";
                    break;
                case "tags_desc":
                    jobsIQ = jobsIQ.OrderByDescending(s => s.Tags);
                    tagsSort = "tags_asc";
                    break;
                case "tags_asc":
                    jobsIQ = jobsIQ.OrderBy(s => s.Tags);
                    tagsSort = "tags_desc";
                    break;
                case "expirationDate_desc":
                    jobsIQ = jobsIQ.OrderByDescending(s => s.AddedDate);
                    addedDateSort = "expirationDate_asc";
                    break;
                case "expirationDate_asc":
                    jobsIQ = jobsIQ.OrderBy(s => s.AddedDate);
                    addedDateSort = "expirationDate_desc";
                    break;
                default:
                    jobsIQ = jobsIQ.OrderBy(s => s.Description);
                    break;
            }

            JobOffer = jobsIQ.AsNoTracking().ToList();
            bool anyOfferFound = JobOffer.Count() > 0;
            if (jobId == 0 && anyOfferFound)
            {
                JobOfferSingle = JobOffer.First();
            }
            else if (anyOfferFound)
            {
                JobOfferSingle = JobOffer.Where(jo => jo.Id == jobId).First();
            }
        }
    }
}
