using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketProgram
{
    public class Payload
    {
        public int PayloadID { get; set; }
        public string PayloadName { get; set; }
        public int NumberOfSatellites { get; set; }
        public string Manufacturer { get; set; }
        public enum DestinationOrbit { SSO, LEO, GEO, GTO, POL }
        public virtual Mission Mission { get; set; }
    }
}
