using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MIRAHUB.Models
{
    public class MIRAHUBDb : IdentityDbContext<AppUser>
    {
        public MIRAHUBDb(DbContextOptions<MIRAHUBDb> options) : base(options)
        {
        }
        public DbSet<Accounts> Accounts { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
