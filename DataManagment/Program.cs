using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketProgram;

namespace DataManagment
{
    class Program
    {
        static void Main(string[] args)
        {
            MissionData mdb = new MissionData();

            using(mdb)
            {
                Mission m1 = new Mission() 
                {   MissionID = 1,
                    MissionName = "Starlink 2",
                    MissionDescription = "A SpaceX Falcon 9 rocket launched the third batch of 60 satellites for SpaceX’s Starlink broadband network, a mission designated Starlink 2.",
                    LaunchDate = new DateTime(06/01/2020),
                    LaunchSite = "Cape Canaveral Air Force Station, Florida",
                    RocketID = 1,
                };
                Rocket falcon9 = new Rocket()
                {
                    RocketID = 1,
                    RocketName = "Falcon 9",
                    Manufacturer = "SpaceX",
                    Description = "Falcon 9 is a partially reusable two-stage-to-orbit medium lift launch vehicle designed and manufactured by SpaceX in the United States. It is powered by Merlin engines, also developed by SpaceX, burning cryogenic liquid oxygen and rocket-grade kerosene (RP-1) as propellants."

                };
            }

        }
    }
}
