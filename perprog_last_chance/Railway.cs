using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace perprog_last_chance
{
    class Railway
    {
        public string NameOfTheRailway { get; private set; }
        public bool IsOccupied { get; set; }

        public Railway(string nameoftherailway, bool isoccupied)
        {
            this.NameOfTheRailway = nameoftherailway;
            this.IsOccupied = isoccupied;
        }
    }
}
