using Microsoft.EntityFrameworkCore;
using PaintingAndSound.Entities;
using System.Collections.Generic;

namespace PaintingAndSound.ORM
{
    public class HSDbContext : DbContext
    {
        public HSDbContext(DbContextOptions<HSDbContext> options)
          : base(options)
        {

        }
        public ISet<Fans> Fans { get; set; }
        public DbSet<Painting> Paintings { get; set; }
        public DbSet<PaintingComment> PaintingComments { get; set; }
        public DbSet<Radio> Radios { get; set; }
        public DbSet<RadioComment> RadioComments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Team { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HSDemo");
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
