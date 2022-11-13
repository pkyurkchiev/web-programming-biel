using System.Collections.Generic;

namespace PM.WebServices.Models
{
    public class Parking
    {
        public string Name { get; set; }
        public bool IsOpened { get; set; }

        public List<ParkingPlace> Places { get; set; }
    }
}
