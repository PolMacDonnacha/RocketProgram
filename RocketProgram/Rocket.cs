using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketProgram
{
    public class Rocket
    {
        public int RocketID { get; set; }
        public string RocketName { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public virtual Mission Mission { get; set; }

        public int MissionID { get; set; }
    }
    public class Payload
    {
        public int PayloadID { get; set; }
        public string PayloadName { get; set; }
        public int NumberOfSatellites { get; set; }
        public string Manufacturer { get; set; }
        public enum DestinationOrbit {SSO,LEO,GEO,GTO,POL }
        public virtual Mission Mission { get; set; }
    } 
    public class Mission
    {
        public int MissionID { get; set; }
        public string MissionName { get; set; }
        public string MissionDescription { get; set; }
        public DateTime LaunchDate { get; set; }
        public int RocketID { get; set; }
        public virtual List<Payload> Payloads { get; set; }
    }

    public class MissionData : DbContext
    {
        public MissionData() : base("MissionData") { }
        public DbSet<Rocket> Rockets { get; set; }
        public DbSet<Payload> Payloads { get; set; }
        public DbSet<Mission> Missions { get; set; }
    }
}
