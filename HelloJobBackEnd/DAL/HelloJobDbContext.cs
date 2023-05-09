using HelloJobBackEnd.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelloJobBackEnd.DAL
{
	public class HelloJobDbContext : IdentityDbContext<User>
	{


		public HelloJobDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}
