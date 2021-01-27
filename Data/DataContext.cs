using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<WebApp.Models.JobApplication> JobApplication { get; set; }

        public DbSet<WebApp.Models.DatabaseFile> DatabaseFile { get; set; }

        public DbSet<WebApp.Models.JobOffer> JobOffer { get; set; }
    }
}
