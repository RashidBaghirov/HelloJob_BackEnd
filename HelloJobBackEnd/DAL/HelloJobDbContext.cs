using HelloJobBackEnd.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace HelloJobBackEnd.DAL
{
    public class HelloJobDbContext : IdentityDbContext<User>
    {


        public HelloJobDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Cv> Cvs { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<OperatingMode> OperatingModes { get; set; }
        public DbSet<BusinessArea> BusinessArea { get; set; }
        public DbSet<BusinessTitle> BusinessTitle { get; set; }
        public DbSet<Vacans> Vacans { get; set; }
        public DbSet<InfoEmployeer> InfoEmployeers { get; set; }
        public DbSet<InfoWork> InfoWorks { get; set; }
        public DbSet<Company> Companies { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().
                    HasIndex(s => s.Name).
                    IsUnique();
            base.OnModelCreating(modelBuilder);
        }




    }
}
