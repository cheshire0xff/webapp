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
        public DatabaseFile CvFile { get; set;}
        private async Task getFile()
        {
            if (!User.IsInRole("Employer"))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                try 
                {
                    CvFile = _context.DatabaseFile.First(f => f.UserId == user.Id);
                }
                catch (InvalidOperationException)
                {
                    CvFile = null;
                }
            }
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null || User == null || User.IsInRole("Employer"))
            {
                return Forbid();
            }
            await getFile();
            return Page();
        }
        public async Task<ActionResult> OnGetDownloadAsync()
        {
            await getFile();
            return File(CvFile.Content, "application/pdf");
        }
        public async Task<ActionResult> OnGetDeleteAsync()
        {
            if (!User.IsInRole("Employer"))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                try 
                {
                    CvFile = _context.DatabaseFile.First(f => f.UserId == user.Id);
                    _context.DatabaseFile.Remove(CvFile);
                    CvFile = null;
                    await _context.SaveChangesAsync();
                }
                catch (InvalidOperationException)
                {
                    CvFile = null;
                }
            }
            return Page();
        }
    }
}
