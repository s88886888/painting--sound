using Microsoft.EntityFrameworkCore;
using PaintingAndSound.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PaintingAndSound.ORM
{
    public class HSDbContext : DbContext
    {
        public HSDbContext(DbContextOptions<HSDbContext> options)
          : base(options)
        {

        }
        public DbSet<Fans> Fans { get; set; }
        public DbSet<Painting> Paintings { get; set; }
        public DbSet<Works> Works { get; set; }
        public DbSet<Radio> Radios { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<WorksComments> WorksComments { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<PaintionPhotos> PaintionPhotos { get; set; }
        public DbSet<FansAndUser> FansAndUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTeam>().HasKey(a => new { a.UserId, a.TeamId });
            modelBuilder.Entity<FansAndUser>().HasKey(a => new { a.UserId, a.FansId });


            //modelBuilder.Entity<Works>().HasOne(l => l.Paintings).WithOne(l => l.Works)
            //   .HasForeignKey<Works>(l => l.PaintingId);
            //modelBuilder.Entity<Works>().HasOne(l => l.Radios).WithOne(l => l.Works)
            //   .HasForeignKey<Works>(l => l.RadiosId);



            //var foreignKeys = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()).Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade);
            //foreach (var fk in foreignKeys)
            //{
            //    fk.DeleteBehavior = DeleteBehavior.Restrict;
            //}
//            modelBuilder.Entity<Works>()
//.HasOne(x => x.User)
//.WithMany(x => x.Works)
//.HasForeignKey(x => x.RadioId).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Painting>()
            //    .HasOne(a => a.User)
            //    .WithOne(a => a.Painting)
            //    .HasForeignKey<Painting>(a => a.User.Id);


            //modelBuilder.Entity<Fans>()
            //    .HasOne(a => a.Users)
            //    .WithMany(a => a.Fans)
            //    .HasForeignKey(a => a.UserId);

            //modelBuilder.Entity<WorksComments>()
            //    .HasOne(a => a.Users)
            //    .WithMany(a => a.WorksComments)
            //    .HasForeignKey(a => a.UserId);

            //modelBuilder.Entity<Works>()
            //    .HasOne(a => a.User)
            //    .WithMany(a => a.Works)
            //    .HasForeignKey(a => a.UserId);

            //modelBuilder.Entity<WorksComments>()
            //    .HasOne(a => a.Works)
            //    .WithMany(a => a.WorksComments)
            //    .HasForeignKey(a => a.WorksId);

            //modelBuilder.Entity<Painting>()
            //    .HasOne(a => a.Works)
            //    .WithMany(a => a.Paintings)
            //    .HasForeignKey(a => a.WorksId);
        }
    }
}
