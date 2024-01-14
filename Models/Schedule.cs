using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalConsole.Models
{
    public class Schedule
    {

        public DateOnly pickupDate { get; set; }

        public DateOnly dropOffDate { get; set; }

        public Driver driver { get; set; }

        public double totalPrice { get; set; } = 0;


        

    }
}
