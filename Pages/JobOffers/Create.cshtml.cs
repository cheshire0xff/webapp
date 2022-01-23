using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;
using WebApp.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Cryptography;

namespace WebApp.Pages.JobOffers
{

    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name="File")]
        public IFormFile FormFile { get; set; }
    }
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

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null || (!User.IsInRole("Employer") && !User.IsInRole("Administrator")))
            {
                return Forbid();
            }

            return Page();
        }
        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }
        [BindProperty]
        public JobOffer JobOffer { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!FileUpload.FormFile.FileName.EndsWith(".pdf"))
            {
                ModelState.AddModelError("File", "Invalid file format.");
                return Page();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }

            if (user == null || (!User.IsInRole("Employer") && !User.IsInRole("Administrator")))
            {
                return Forbid();
            }

            using (var memoryStream = new MemoryStream())
            {
                await FileUpload.FormFile.CopyToAsync(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    using (SHA1 sha1 = SHA1.Create())
                    {
                        var file = new DatabaseFile()
                        {
                            Content = memoryStream.ToArray(),
                            Hash = sha1.ComputeHash(memoryStream.ToArray()),
                            UserId = user.Id
                        };

                        _context.DatabaseFile.Add(file);
                        await _context.SaveChangesAsync();
                        JobOffer.FileId = file.Id;
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                    return Page();
                }
            }
            JobOffer.EmployerId = user.Id;
            JobOffer.AddedDate = DateTime.Now;
            if ((JobOffer.ExpirationDate - JobOffer.AddedDate).TotalDays < 0)
            {
                ModelState.AddModelError("Date", "Expiration date has to be at least one day from current time!");
                return Page();
            }
            ModelState.Clear();
            if (!TryValidateModel(JobOffer, nameof(JobOffer)))
            {
                return Page();
            }
            _context.JobOffer.Add(JobOffer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
