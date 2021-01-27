using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Pages.CV
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
        public DatabaseFile cvFile { get; set;}
        public async Task OnGetAsync()
        {
            if (!User.IsInRole("Employer"))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                try 
                {

                cvFile = _context.DatabaseFile.First(f => f.UserId == user.Id);
                }
                catch (InvalidOperationException)
                {
                    cvFile = null;
                }
            }
        }
        public ActionResult OnGetDownloadAsync()
        {
            return File(cvFile.Content, "application/octet-stream", "cv.pdf");
        }
        public async Task<ActionResult> OnGetDeleteAsync()
        {
            if (!User.IsInRole("Employer"))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                try 
                {
                    cvFile = _context.DatabaseFile.First(f => f.UserId == user.Id);
                    _context.DatabaseFile.Remove(cvFile);
                    cvFile = null;
                    await _context.SaveChangesAsync();
                }
                catch (InvalidOperationException)
                {
                    cvFile = null;
                }
            }
            return Page();
        }
    }
}
