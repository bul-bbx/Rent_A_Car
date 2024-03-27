using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Rent_A_Car.Areas.Identity.Data;
using System.Data.OleDb;
using System.Reflection.Emit;
using System.Runtime.Versioning;

using Microsoft.AspNetCore.Hosting;
using Rent_A_Car.ViewModels.Car;

namespace Rent_A_Car.Data;

public class DbContext : IdentityDbContext<User>
{

    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Reservation>()
                .HasOne(c => c.Car)
                .WithMany(r => r.Reservations)
                .HasForeignKey(c => c.CarId);
        builder.Entity<Reservation>()
            .HasOne(u => u.User)
            .WithMany(r => r.Reservations)
            .HasForeignKey(u => u.UserId);

    }
}
