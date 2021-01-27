using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApp.Data;
using WebApp.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Pages.CV
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

        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostUploadAsync()
        {
            if (!FileUpload.FormFile.FileName.EndsWith(".pdf"))
            {
                ModelState.AddModelError("File", "Invalid file format.");
                return Page();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
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
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }
            return Page();
        }
    }
}
