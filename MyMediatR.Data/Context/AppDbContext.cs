using Microsoft.EntityFrameworkCore;
using MyMediatR.Data.Entities;

namespace MyMediatR.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(x =>
        {
            x.HasKey(o => o.CustomerId);
            x.HasIndex(o => o.CustomerId);
            x.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            x.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        });
    }

    public DbSet<Customer> Customers { get; set; }
}