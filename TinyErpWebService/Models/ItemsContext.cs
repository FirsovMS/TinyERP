﻿using Microsoft.EntityFrameworkCore;
using TinyErpWebService.Models.DTO;

namespace TinyErpWebService.Models
{
    public class ItemsContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public ItemsContext(DbContextOptions<ItemsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("orders");
            base.OnModelCreating(modelBuilder);
        }
    }
}
