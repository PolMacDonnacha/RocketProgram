using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketProgram;

namespace DataManagment
{
    public class Program
    {
        static void Main(string[] args)
        {
            MissionData mdb = new MissionData();

            using(mdb)
            {

               /* Mission m8 = new Mission() 
                {   MissionID = 8,
                    MissionName = "Crew Dragon Demo-2",
                    MissionDescription = "SpaceX will launch NASA Astronauts Doug Hurley and Bob Behnken to the International Space Station. This will be the first crewed flight of American rockets from American soil since 2011.",
                    LaunchDate = new DateTime(2020,05,27),
                    LaunchSite = "LC-39A, Kennedy Space Center, Florida",
                    RocketID = 1
                };
                Mission m7 = new Mission()
                {
                    MissionID = 7,
                    MissionName = "Noor",
                    MissionDescription = "A SpaceX Falcon 9 rocket launched the seventh batch of approximately 60 satellites for SpaceX’s Starlink broadband network, a mission designated Starlink 6.",
                    LaunchDate = new DateTime(2020, 04, 22),
                    LaunchSite = "Shahroud Missile Range, Iran",
                    RocketID = 4
                };
                Rocket Qased = new Rocket()
                {
                    RocketID = 4,
                    RocketName = "Qased",
                    Manufacturer = "Iran’s Revolutionary Guard Corps",
                    Description = "Qased is the third Iranian space launcher. It is apparently based on the Shahab-3 SRBM or Emad SRBM missile similar in size to the Safir rocket. Qased has been described as a three stage launch vehicle using liquid- and solid-fueled stages.",
                    Image = "qased.jpg",
                    CountryOfOrigin = "Iran",
                };*/
                Payload dougbob = new Payload()
                {
                    PayloadID = 12,
                    PayloadName = "Doug Hurley & Bob Behnken",
                    NumberOfSatellites = 2,
                    Manufacturer = "NASA",
                    DestinationOrbit = "International Space Station",
                    Description = "NASA Astroauts Doug Hurley & Bob Behnken will spend up to 110 days aboard the international space station",
                    MissionID = 8,
                   
                    Image="dougbob.jpg"
                };
                
               /* Payload Nusat8 = new Payload()
                {
                    PayloadID = 5,
                    PayloadName = "NuSat 8",
                    NumberOfSatellites = 1,
                    Manufacturer = "Satellogic",
                    DestinationOrbit = "Sun Synchronous Orbit",
                    Description = "NuSat satellites are equipped with an imaging system operating in visible light and infrared. The constellation will allow for commercially available real-time Earth imaging and video with a ground resolution of 3.3 ft (1 m).",
                    MissionID = 3,
                    Mission = m3,
                    Image = "starlink.jpg"
                };*/
                
               // mdb.Missions.Add(m8);
                //  mdb.Missions.Add(m7);

                //mdb.Rockets.Add(electron);

                 mdb.Payloads.Add(dougbob);
                //  mdb.Payloads.Add(Nusat8);

                 mdb.SaveChanges();
               

                Console.WriteLine("Saved to database");

            }

        }
    }
}
