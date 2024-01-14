using CarRental.Interfaces;
using CarRental.Repositories;
using CarRental.Services;
using CarRentalConsole.Models;
using CarRentalConsole.Models.Abstract;
using CarRentalConsole.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalConsole
{
    class Startup
    {


        private static VehicleRepository _repository = new VehicleRepository();

        private static RentalManagerService _manager = new RentalManagerService(_repository);

        private static RentalCustomerService _customer = new RentalCustomerService(_repository);


        public static void main() {
            int type = showMainMenu();


            if (type == 1)
            {
                int adminMenuSelection = showAdminMenu();

                if (adminMenuSelection == 1)
                {


                    Vehicle vehicle = showAddVehicleMenu();
                    _manager.AddVehicle(vehicle);
                    main();


                }
                else if (adminMenuSelection == 2)
                {
                    _manager.ListVehicles();

                    Console.WriteLine("Please enter the vehicle number to be deleted");
                    string vehicleIndex = Console.ReadLine();

                    _manager.DeleteVehicle(vehicleIndex);
                    main();


                }
                else if (adminMenuSelection == 3)
                {
                    _manager.ListVehicles();
                    main();

                }

                else if (adminMenuSelection == 4)
                {
                    _manager.ListOrderedVehicles();
                    main();

                }
                else if (adminMenuSelection == 5)
                {

                    _manager.GenerateReport("C:\\Users\\jonat\\OneDrive\\Desktop\\myapp\\testfile.txt");
                    main();
                }
                else
                {
                    main();
                }


            }
            else if (type == 2)
            {

                int customerSelection = showCustomerMenu();


                if (customerSelection == 1)
                {
                    Type vehicleTypeInput = getVehicleType();

                    Schedule scheduleInput = showCreateScheduleMenu();

                    _customer.ListAvailableVehicles(scheduleInput, vehicleTypeInput);

                    main();
                }
                else if (customerSelection == 2)
                {
                    Type vehicleTypeInput = getVehicleType();


                    Schedule scheduleInput = showCreateScheduleMenuWithDriver();

                    _customer.ListAvailableVehicles(scheduleInput, vehicleTypeInput);

                    Console.WriteLine("Please enter the vehicle number to add the reservation to");
                    string vehicleIndex = Console.ReadLine();

                    _customer.AddReservation(vehicleIndex, scheduleInput);

                    main();
                }
                else if (customerSelection == 3)
                {
                    Type vehicleTypeInput = getVehicleType();

                    _customer.ListAvailableVehiclesWithScheduleNumbers(vehicleTypeInput);

                    Console.WriteLine("Please enter the vehicle number to modify the reservation to");
                    string vehicleIndex = Console.ReadLine();

                    Console.WriteLine("Please enter the schedule number to modify");
                    string scheduleIndex = Console.ReadLine();

                    Schedule schedule = _customer.GetShedule(int.Parse(vehicleIndex), int.Parse(scheduleIndex));

                    Schedule scheduleInput = showCreateScheduleMenuWithDriver();

                    _customer.ChangeReservation(vehicleIndex, schedule, scheduleInput);


                    main();
                }
                else if (customerSelection == 4)
                {
                    Type vehicleTypeInput = getVehicleType();

                    _customer.ListAvailableVehiclesWithScheduleNumbers(vehicleTypeInput);

                    Console.WriteLine("Please enter the vehicle number to delete a schedule to");
                    string vehicleIndex = Console.ReadLine();

                    Console.WriteLine("Please enter the schedule number to delete");
                    string scheduleIndex = Console.ReadLine();

                    Schedule schedule = _customer.GetShedule(int.Parse(vehicleIndex), int.Parse(scheduleIndex));


                    _customer.DeleteReservation(vehicleIndex, schedule);


                    main();
                }
                else 
                {
                    main();
                }


            }
            else
            {
                Console.WriteLine("Invalid Selection");
                main();
            }

        }








        private static int showMainMenu()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("Select a Type of User");
            Console.WriteLine("1 - Admin");
            Console.WriteLine("2 - Customer");
            Console.WriteLine("3 - Exit");
            return (int.Parse(Console.ReadLine()));
        }

        private static int showAdminMenu()
        {
            Console.WriteLine("Admin Selected");

            Console.WriteLine("Please Select a Function");
            Console.WriteLine("- - - - - - - - -");

            Console.WriteLine("1 - Add Vehicle");
            Console.WriteLine("2 - Delete Vehicle");
            Console.WriteLine("3 - List Vehicles");
            Console.WriteLine("4 - List Vehicles - Ordered");
            Console.WriteLine("5 - Generate Report");
            Console.WriteLine("6 - Back");

            return (int.Parse(Console.ReadLine()));

        }

        private static int showCustomerMenu()
        {
            Console.WriteLine("Customer Selected");

            Console.WriteLine("Please Select a Function");
            Console.WriteLine("- - - - - - - - -");
            Console.WriteLine("1 - List Vehicles");
            Console.WriteLine("2 - Add Reservation");
            Console.WriteLine("3 - Modify Reservation");
            Console.WriteLine("4 - Delete Reservation");
            Console.WriteLine("5 - Back");

            return (int.Parse(Console.ReadLine()));

        }


        private static Vehicle showAddVehicleMenu()
        {
            Console.WriteLine("Please enter the vehicle type");
            Console.WriteLine("1 - Car");
            Console.WriteLine("2 - Electric Car");
            Console.WriteLine("3 - Van");
            Console.WriteLine("4 - Bike");

            int vehicleTypeInput = int.Parse(Console.ReadLine());

            Vehicle vehicle;
            if (vehicleTypeInput == 1)
            {
                vehicle = new Car();

            }
            else if (vehicleTypeInput == 2)
            {
                vehicle = new ElectricCar();
            }

            else if (vehicleTypeInput == 3)
            {
                vehicle = new Van();
            }
            else if (vehicleTypeInput == 4)
            {
                vehicle = new Bike();
            }
            else
            {
                Console.WriteLine("Invalid Vehicle Selection");
                return null;
            }


            Console.WriteLine("Please enter the vehicle registration");
            String vehicleRegInput = Console.ReadLine();
            Console.WriteLine("Please enter the vehicle make");
            String vehicleMakeInput = Console.ReadLine();
            Console.WriteLine("Please enter the vehicle model");
            String vehicleModelInput = Console.ReadLine();
            Console.WriteLine("Please enter the daily rental price of the vehicle");
            String vehicleDailyRentalInput = Console.ReadLine();


            vehicle.registrationNumber = vehicleRegInput;
            vehicle.make = vehicleMakeInput;
            vehicle.model = vehicleModelInput;
            vehicle.dailyRentalPrice = double.Parse(vehicleDailyRentalInput);

            return vehicle;
        }



        private static Type getVehicleType()
        {
            Console.WriteLine("Please enter the vehicle type");
            Console.WriteLine("1 - Car");
            Console.WriteLine("2 - Electric Car");
            Console.WriteLine("3 - Van");
            Console.WriteLine("4 - Bike");

            int vehicleTypeInput = int.Parse(Console.ReadLine());

            Schedule schedule = new Schedule();

            Type type;

            if (vehicleTypeInput == 1)
            {
                type = typeof(Car);

            }
            else if (vehicleTypeInput == 2)
            {
                type = typeof(ElectricCar);
            }

            else if (vehicleTypeInput == 3)
            {
                type = typeof(Van);

            }
            else if (vehicleTypeInput == 4)
            {
                type = typeof(Bike);

            }
            else
            {
                Console.WriteLine("Invalid Vehicle Selection");
                return null;
            }

            return type;

        }

        private static Schedule showCreateScheduleMenu()
        {
           


            Console.WriteLine("Please enter the Pickup Date in the format YYYY/MM/DD");
            DateOnly pickupDate =   DateOnly.Parse(Console.ReadLine().ToString());


            Console.WriteLine("Please enter the DropoffDate Date in the format YYYY/MM/DD");
            DateOnly dropOffDate = DateOnly.Parse(Console.ReadLine().ToString());



            Schedule schedule = new Schedule();

            schedule.pickupDate   = pickupDate;
            schedule.dropOffDate = dropOffDate;


            return schedule;
        }




        private static Schedule showCreateScheduleMenuWithDriver()
        {



            Console.WriteLine("Please enter the Pickup Date in the format YYYY/MM/DD");
            DateOnly pickupDate = DateOnly.Parse(Console.ReadLine().ToString());


            Console.WriteLine("Please enter the DropoffDate Date in the format YYYY/MM/DD");
            DateOnly dropOffDate = DateOnly.Parse(Console.ReadLine().ToString());

            Console.WriteLine("Please enter the driver's first name");
            String driverFirstName = Console.ReadLine().ToString();

            Console.WriteLine("Please enter the driver's surname");
            String driverSurname = Console.ReadLine().ToString();

            Console.WriteLine("Please enter the driver's license number");
            String driverLicenseNo = Console.ReadLine().ToString();

            Console.WriteLine("Please enter the driver's Date of birth in the format YYYY/MM/DD");
            DateOnly driverDateofBirth = DateOnly.Parse(Console.ReadLine().ToString());

            Schedule schedule = new Schedule();

            Driver driver = new Driver(driverFirstName, driverSurname, driverDateofBirth, driverLicenseNo);

            schedule.pickupDate = pickupDate;
            schedule.dropOffDate = dropOffDate;
            schedule.driver = driver;

            return schedule;
        }

    }


    



}
