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
        private readonly DbContext _authDbContext;

        public CarController(DbContext dbContext)
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
        public IActionResult AddCar([FromForm]Car m)
        {
            if(!ModelState.IsValid)
                return View(m);
            bool isSaved = _authDbContext.AddNewCar(m);
            ViewBag.isSaved = isSaved;
            ModelState.Clear();
            return AddCar();
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
