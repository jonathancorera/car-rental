using CarRental.Interfaces;
using CarRental.Repositories;
using CarRentalConsole.Models;
using CarRentalConsole.Models.Abstract;
using System;

namespace CarRental.Services
{
    public class RentalManagerService : IRentalManager

    {



        private readonly VehicleRepository _vehicleRepository;

        public RentalManagerService(VehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public bool AddVehicle(Vehicle v)
        {
            try
            {

                List<Vehicle> vehicles = _vehicleRepository.getAllVehicles();

                if(vehicles.Count >= 50)
                {
                    Console.WriteLine("Capacity of 50 vehicles reached. Unable to add vehicle");
                }

                _vehicleRepository.addVehicle(v);
                Console.WriteLine("Vehicle Successfully Added");
                Console.WriteLine((vehicles.Count+1) + " Slots Available");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;


        }

        public bool DeleteVehicle(string number)
        {
            int index = int.Parse(number);

            try
            {
                _vehicleRepository.removeVehicleAtIndex(index - 1);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;

        }

        public void GenerateReport(string fileName)
        {
            try
            {
                // Check if file already exists. If yes, delete it.
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                List<Vehicle> vehicles = _vehicleRepository.getAllVehicles();

                using (StreamWriter sw = File.CreateText(fileName))
                {

                    foreach(Vehicle vehicle in vehicles)
                    {
                        sw.WriteLine("Registration Number: {0} - {1} {2}", vehicle.registrationNumber, vehicle.make, vehicle.model);

                        
                        if(vehicle.schedules != null && vehicle.schedules.Count > 0)
                        {
                            foreach (Schedule schedule in vehicle.schedules)
                            {
                                sw.WriteLine("Schedule: Pickup: {0}  Dropoff: {1}    Driver: {2} {3}", schedule.pickupDate.ToString(), schedule.dropOffDate.ToString(), schedule.driver.firstName, schedule.driver.surname);
                              
                            }
                        }
                      

                        sw.WriteLine(" ");
                        sw.WriteLine(" ");

                    }

                }

                // Write file contents on console.
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

        }
    

        public void ListOrderedVehicles()
        {
            List<Vehicle> vehicles = _vehicleRepository.getAllVehicles();

            vehicles.Sort(); 

            for (int i = 0; i < vehicles.Count; i++)
            {

                Console.WriteLine(i + 1 + "- " + vehicles[i].make + " " + vehicles[i].model);
                Console.WriteLine("Schedules");
                if (vehicles[i].schedules.Count > 0)
                {
                    foreach (Schedule schedule in vehicles[i].schedules)
                    {
                        Console.WriteLine("Pickup: " + schedule.pickupDate.ToString() + "  Dropoff: " + schedule.dropOffDate.ToString());

                    }
                }
                else
                {
                    Console.WriteLine("None");
                }
               
                Console.WriteLine("");
            }
        }

        public void ListVehicles()
        {

            List<Vehicle> vehicles = _vehicleRepository.getAllVehicles();

            Console.WriteLine("");
            Console.WriteLine("Available Vehicles");


            for (int i = 0; i < vehicles.Count; i++)
            {

                Console.WriteLine(i + 1 + "- " + vehicles[i].make + " " + vehicles[i].model);
                Console.WriteLine("Schedules");
                if (vehicles[i].schedules.Count > 0)
                {
                    foreach (Schedule schedule in vehicles[i].schedules)
                    {
                        Console.WriteLine("Pickup: " + schedule.pickupDate.ToString() + "  Dropoff: " + schedule.dropOffDate.ToString());

                    }
                }
                else
                {
                    Console.WriteLine("None");
                }
                Console.WriteLine("");
            }
        }
    }
    }
