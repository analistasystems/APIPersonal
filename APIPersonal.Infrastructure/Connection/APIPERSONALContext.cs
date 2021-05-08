using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIPersonal.Infrastructure.Connection
{
    public class APIPERSONALContext : DbContext
    {
        public APIPERSONALContext(DbContextOptions<APIPERSONALContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
