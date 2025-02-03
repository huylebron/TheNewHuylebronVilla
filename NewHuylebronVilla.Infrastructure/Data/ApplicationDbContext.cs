﻿using Microsoft.EntityFrameworkCore;
using NewHuylebronVilla.Domain.Entities;

namespace NewHuylebronVilla.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Villa> Villas { get; set; }
    }
}