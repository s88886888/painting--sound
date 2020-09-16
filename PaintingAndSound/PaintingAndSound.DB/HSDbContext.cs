using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.DB
{
    public class HSDbContext : DbContext
    {
        public HSDbContext() : base()
        {
        }
        public HSDbContext(DbContextOptions<HSDbContext> options)
          : base(options)
        {

        }
        public DbSet<Fans> Fans { get; set; }
        public DbSet<Painting> Paintings { get; set; }
        public DbSet<PaintingfComment> PaintingfComments { get; set; }
        public DbSet<Radio> Radios { get; set; }
        public DbSet<RadioComment> RadioComments { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("ConnectionStrings");
            }
        }
    }
}
