using CarRentalConsole.Models;
using CarRentalConsole.Models.Abstract;

namespace CarRental.Repositories
{
    public class VehicleRepository
    {

        private List<Vehicle> vehicles { get; set; } = new List<Vehicle>();


        public VehicleRepository()
        {

            Driver driver1 = new Driver("John", "Doe", new DateOnly(1992, 01, 01), "12345"); 
            Driver driver2 = new Driver("Jane", "Doe", new DateOnly(1992, 03, 05), "67890");
            Driver driver3 = new Driver("Jeremy", "Doe", new DateOnly(1990, 04, 24), "11111");



            Vehicle car1 = new Car();
            car1.registrationNumber = "1111111111";
            car1.make = "Honda";
            car1.model = "Civic";
            car1.dailyRentalPrice = 99.99;


            Schedule car1Scedule1 = new Schedule();
            car1Scedule1.pickupDate = new DateOnly(2024, 01, 01);
            car1Scedule1.dropOffDate = new DateOnly(2024, 01, 06);
            car1Scedule1.driver = driver1;

            car1.schedules.Add(car1Scedule1);



            Vehicle car2 = new Car();
            car2.registrationNumber = "2222222222";
            car2.make = "Toyota";
            car2.model = "Camry";
            car2.dailyRentalPrice = 89.99;


            Vehicle van1 = new Van();
            van1.registrationNumber = "3333333333";
            van1.make = "Toyota";
            van1.model = "Hiace";
            van1.dailyRentalPrice = 129.99;


            Vehicle elecCar1 = new ElectricCar();
            elecCar1.registrationNumber = "4444444444";
            elecCar1.make = "Tesla";
            elecCar1.model = "Model S";
            elecCar1.dailyRentalPrice = 499.99;


            Vehicle bike1 = new Bike();
            bike1.registrationNumber = "5555555555";
            bike1.make = "Vincent";
            bike1.model = "Black Shadow";
            bike1.dailyRentalPrice = 199.99;

            vehicles.AddRange(new List<Vehicle> { car1, car2, bike1, elecCar1, van1 });


        }

        public List<Vehicle>  getAllVehicles ()
        {
            return vehicles;
        }

        public void addVehicle (Vehicle vehicle)
        {
            this.vehicles.Add(vehicle);
        }


        public void removeVehicle (Vehicle vehicle) { 
            this.vehicles.Remove(vehicle);  
        }

        public void removeVehicleAtIndex(int index)
        {
            this.vehicles.RemoveAt(index);
        }

        public void updateVehicle (int vehicleIndex, Vehicle newVehicle)
        {
            this.vehicles.RemoveAt(vehicleIndex);

            this.vehicles.Insert(vehicleIndex, newVehicle);
        }


       
    }
}
