using Microsoft.AspNetCore.Mvc;

namespace Rent_A_Car.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
