using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Rent_A_Car.Data;
using Rent_A_Car.ViewModels.Car;
using System.Data.OleDb;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Build.Framework;
using Rent_A_Car.Repository;

namespace Rent_A_Car.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarController : Controller
    {
        private readonly DbContext _authDbContext;
        private readonly IData _data;

        public CarController(DbContext dbContext, IData _data)
        {
            _authDbContext = dbContext;
            this._data = _data;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult AddCar([FromForm]Car model)
        {
            if(!ModelState.IsValid)
                return View(model);
            bool isSaved = _data.AddNewCar(model);
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
