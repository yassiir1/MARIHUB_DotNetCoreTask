using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MIRAHUB.Models
{
    public class MIRAHUBDb : IdentityDbContext<AppUser>
    {
        protected readonly IConfiguration Configuration;
        public MIRAHUBDb(DbContextOptions<MIRAHUBDb> options , IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        public DbSet<Accounts> Accounts { get; set; } = default!;
        public DbSet<OrderDetails> OrderDetails { get; set; } 
        public DbSet<Orders> Orders { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("MIRAHUBDb"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
