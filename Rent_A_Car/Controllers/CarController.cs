using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Rent_A_Car.Data;
using Rent_A_Car.ViewModels.Car;
using System.Data.OleDb;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Hosting;

namespace Rent_A_Car.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarController : Controller
    {
        private readonly AuthDbContext _authDbContext;

        public CarController(AuthDbContext dbContext)
        {
            _authDbContext = dbContext;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCar([FromForm]AddCarViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            bool isSaved = _authDbContext.AddNewCar(model);
            ViewBag.isSaved = isSaved;
            ModelState.Clear();
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditCar() 
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCar() 
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult ListCar() 
        {
            var cars = _authDbContext.Cars.ToList();
            return View(cars);
        }
    }
}
