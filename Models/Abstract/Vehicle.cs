using CarRentalConsole.Interfaces;

namespace CarRentalConsole.Models.Abstract
{
    public abstract class Vehicle : IComparable<Vehicle>, IOverlappable
    {

        public string registrationNumber { get; set; }

        public string make { get; set; }

        public string model { get; set; }

        public double dailyRentalPrice { get; set; }

        public List<Schedule> schedules { get; set; } = new List<Schedule>();

        public int CompareTo(Vehicle? other)
        {
            return make.CompareTo(other.make);
        }

        public bool Overlaps(Schedule other)
        {
            foreach (var schedule in this.schedules)
            {
                if(other.pickupDate >= schedule.pickupDate && other.pickupDate <= schedule.dropOffDate)
                {
                    return true;
                }
              
            }

            return false;
        }
    }
}
