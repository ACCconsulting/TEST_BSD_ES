using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
   public class AplicationContext:DbContext
    {
        public AplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicle { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
