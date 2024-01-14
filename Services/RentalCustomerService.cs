using CarRental.Repositories;
using CarRentalConsole.Interfaces;
using CarRentalConsole.Models;
using CarRentalConsole.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalConsole.Services
{
    internal class RentalCustomerService : IRentalCustomer
    {


        private readonly VehicleRepository _vehicleRepository;

        public RentalCustomerService(VehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;

        }

        public bool AddReservation(string number, Schedule wantedSchedule)
        {
            try
            {
                List<Vehicle> vehicles = _vehicleRepository.getAllVehicles();
                int index = int.Parse(number);

                Vehicle vehicle = vehicles[index - 1];


                int rentalNoDays = (wantedSchedule.dropOffDate.DayNumber - wantedSchedule.pickupDate.DayNumber);

                double totalCost = vehicle.dailyRentalPrice * rentalNoDays;


                wantedSchedule.totalPrice = totalCost;

                vehicle.schedules.Add(wantedSchedule);

                vehicles[index - 1] = vehicle;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return false; 

        }

        public bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule)
        {
            try
            {
                List<Vehicle> vehicles = _vehicleRepository.getAllVehicles();
                int index = int.Parse(number);

                Vehicle vehicle = vehicles[index - 1];


                int rentalNoDays = (newSchedule.dropOffDate.DayNumber - newSchedule.pickupDate.DayNumber);

                double totalCost = vehicle.dailyRentalPrice * rentalNoDays;

                newSchedule.totalPrice = totalCost;

                vehicle.schedules.Remove(oldSchedule);
                vehicle.schedules.Add(newSchedule);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Successfully modified schedule");
                Console.WriteLine(ex.ToString());
            }

            return false; 
        }

        public bool DeleteReservation(string number, Schedule schedule)
        {
            try
            {
                List<Vehicle> vehicles = _vehicleRepository.getAllVehicles();
                int index = int.Parse(number);

                Vehicle vehicle = vehicles[index - 1];


                vehicle.schedules.Remove(schedule);

                Console.WriteLine("Successfully removed schedule");
                
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            return false;
        }

        public void ListAvailableVehicles(Schedule wantedSchedule, Type type)
        {
           List<Vehicle> vehicles =  _vehicleRepository.getAllVehicles();

          //  List<Vehicle> vehicles = allVehicles.FindAll(v => v.GetType() == type && !v.Overlaps(wantedSchedule));

            

            Console.WriteLine("");
            Console.WriteLine("Available Vehicles");

            for (int i = 0; i < vehicles.Count; i++)
            {

                if (vehicles[i].GetType() == type && !vehicles[i].Overlaps(wantedSchedule))
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


            Console.WriteLine("");


        }




        public void ListAvailableVehiclesWithScheduleNumbers( Type type)
        {
            List<Vehicle> vehicles = _vehicleRepository.getAllVehicles();


            Console.WriteLine("");
            Console.WriteLine("Available Vehicles With Schedules");

            for (int i = 0; i < vehicles.Count; i++)
            {

                if (vehicles[i].GetType() == type && vehicles[i].schedules.Count > 0)
                {


                    Console.WriteLine(i + 1 + "- " + vehicles[i].make + " " + vehicles[i].model);
                    Console.WriteLine("Schedules");
                    if (vehicles[i].schedules.Count > 0)
                    {
                        for (int j = 0; j < vehicles[i].schedules.Count();  j++)
                        {
                            Schedule schedule = vehicles[i].schedules[j];

                            Console.WriteLine((j+1) + ": Pickup: " + schedule.pickupDate.ToString() + "  Dropoff: " + schedule.dropOffDate.ToString());

                        }
                    }
                    else
                    {
                        Console.WriteLine("None");
                    }
                    Console.WriteLine("");

                }
            }


            Console.WriteLine("");


        }

        internal Schedule GetShedule(int vehicleIndex, int scheduleIndex)
        {
            List<Vehicle> vehicles = _vehicleRepository.getAllVehicles();

            Vehicle vehicle = vehicles[vehicleIndex - 1];

            return vehicle.schedules[scheduleIndex - 1];




        }
    }
}
