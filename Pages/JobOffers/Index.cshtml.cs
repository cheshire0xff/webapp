using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreApp.Data;
using CoreApp.Models;

namespace WebApp.Pages.JobOffers
{
    [Authorize]
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly CoreApp.Data.JobOfferContext _context;

        public IndexModel(CoreApp.Data.JobOfferContext context)
        {
            _context = context;
        }

        public IList<JobOffer> JobOffer { get;set; }

        public async Task OnGetAsync()
        {
            JobOffer = await _context.JobOffer.ToListAsync();
        }
    }
}
