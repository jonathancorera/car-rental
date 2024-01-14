using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalConsole.Models
{
     public class Driver
    {
        public Driver(String fn, String ln, DateOnly dob, String lsn)
        {
            this.firstName = fn;
            this.surname = ln;
            this.dateOfBirth = dob;
            this.licenseNo = lsn;
        }

        public String firstName { get; set; }

        public String surname { get; set; }

        public DateOnly dateOfBirth { get; set; }   

        public String licenseNo { get; set; }   
    }
}
