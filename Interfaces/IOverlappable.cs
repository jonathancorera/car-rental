using CarRentalConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalConsole.Interfaces
{
    internal interface IOverlappable
    {
        bool Overlaps(Schedule other);
    }
}
