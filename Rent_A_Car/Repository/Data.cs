using Rent_A_Car.Data;
using Rent_A_Car.ViewModels.Car;
using System.Data.OleDb;
using System.Runtime.Versioning;

namespace Rent_A_Car.Repository
{
    public class Data : IData
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webhost;

        public Data(IConfiguration configuration, IWebHostEnvironment webhost)
        {
            _configuration = configuration;
            _webhost = webhost;
        }
        [SupportedOSPlatform("windows")]
        public bool AddNewCar(AddCarViewModel newcar)
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
}

