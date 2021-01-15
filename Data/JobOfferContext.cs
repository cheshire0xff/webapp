using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreApp.Models;

namespace CoreApp.Data
{
    public class JobOfferContext : DbContext
    {
        public JobOfferContext (DbContextOptions<JobOfferContext> options)
            : base(options)
        {
        }

        public DbSet<CoreApp.Models.JobOffer> JobOffer { get; set; }
    }
}
