﻿using Microsoft.EntityFrameworkCore;

namespace JsLocalization.Models
{
    public class LocalizationContext : DbContext
    {
        public LocalizationContext(DbContextOptions<LocalizationContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Culture> Cultures { get; set; }
    }
}
