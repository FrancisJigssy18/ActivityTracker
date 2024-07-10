using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ActivityTrackerContext : DbContext
    {
        public ActivityTrackerContext() { }
        public ActivityTrackerContext(DbContextOptions<ActivityTrackerContext> options) : base(options) { }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<RunningActivity> RunningActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().ToTable("UserProfiles");
            modelBuilder.Entity<RunningActivity>().ToTable("RunningActivities");
        }
    }
}
