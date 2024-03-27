using Rent_A_Car.Data;
using Rent_A_Car.ViewModels.Car;

namespace Rent_A_Car.Repository
{
    public interface IData
    {
        bool AddNewCar(AddCarViewModel newcar);
    }
}
