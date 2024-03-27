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
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webhost;
    public DbContext(DbContextOptions<DbContext> options, IConfiguration configuration, IWebHostEnvironment webHost)
        : base(options)
    {
        this._configuration = configuration;
        this._webhost = webHost;
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
    [SupportedOSPlatform("windows")]
    public bool AddNewCar(Car newcar)
    {
        bool isSaved = false;
        OleDbConnection con = GetOleDbConnection();
        try
        {
            con.Open();
            newcar.CarImageUrl = UploadImage(newcar.CarImage, "cars");
            string qry = String.Format("Insert into Cars(RegistrationNumber, Brand, Model, Year, PassengerCapacity, Description, RentalPricePerDay, CarImageUrl) values(" +
                "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", newcar.RegistrationNumber, newcar.Brand, newcar.Model,
                newcar.Year, newcar.PassengerCapacity, newcar.Description, newcar.RentalPricePerDay, newcar.CarImageUrl);
            isSaved = SaveData(qry, con);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
        return isSaved;
    }

    public string UploadImage(IFormFile file, string folderName)
    {
        string imagepath = "";
        try
        {
            string uploadFolder = Path.Combine(_webhost.WebRootPath, "Images/" + folderName);
            imagepath = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filepath = Path.Combine(uploadFolder, imagepath);
            using (var filestream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyTo(filestream);
            }
        }
        catch (Exception)
        {
            throw;
        }
        return imagepath;
    }

    [SupportedOSPlatform("windows")]
    private OleDbConnection GetOleDbConnection()
    {
        return new OleDbConnection(this._configuration.GetConnectionString("DbContextConnection"));
    }
    [SupportedOSPlatform("windows")]
    private bool SaveData(string qry, OleDbConnection con)
    {
        bool isSaved = false;
        try
        {
            OleDbCommand cmd = new OleDbCommand(qry, con);
            cmd.ExecuteNonQuery();
            isSaved = true;
        }
        catch (Exception)
        {
            throw;
        }
        return isSaved;
    }
}
