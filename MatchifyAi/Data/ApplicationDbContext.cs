using MatchifyAI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MatchifyAI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}
