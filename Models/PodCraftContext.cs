using System;
using Microsoft.EntityFrameworkCore;

namespace PodCraft.Models
{
    public class PodCraftContext : DbContext
    {
        public PodCraftContext(DbContextOptions<PodCraftContext> options)
            : base(options)
        {
        }
        public DbSet<PodCraftUser> PodCraftUsers { get; set; }
        public DbSet<PodCraftProduct> PodCraftProducts { get; set; }
        public DbSet<PodCraftSearch> PodCraftSearch { get; set; }
    }
}
