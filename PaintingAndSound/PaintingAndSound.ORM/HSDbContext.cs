﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Fans> Fans { get; set; }
        public DbSet<Painting> Paintings { get; set; }
        public DbSet<Works> Works { get; set; }
        public DbSet<Radio> Radios { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<WorksComments> WorksComments { get; set; }
        public DbSet<UserTeam>  UserTeams { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTeam>().HasKey(a => new { a.UserId, a.TeamId });

            //modelBuilder.Entity<Radio>()
            //    .HasOne(a => a.Works)
            //    .WithOne(a => a.Radio)
            //    .HasForeignKey<Radio>(a => a.Works.Id);


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
