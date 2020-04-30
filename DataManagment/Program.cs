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
                Mission m7 = new Mission() 
                {   MissionID = 7,
                    MissionName = "Mars 2020",
                    MissionDescription = "A United Launch Alliance Atlas 5 rocket will launch NASA’s Mars 2020 rover to the Red Planet. After landing in February 2021, the Mars 2020 rover will study Martian geology, search for organic compounds, demonstrate the ability to generate oxygen from atmospheric carbon dioxide, and collect rock samples for return to Earth by a future mission.",
                    LaunchDate = new DateTime(2020,07,17),
                    LaunchSite = "SLC-41, Cape Canaveral Air Force Station, Florida",
                    RocketID = 6
                };
               /* Mission m7 = new Mission()
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
                Payload marsrover = new Payload()
                {
                    PayloadID = 10,
                    PayloadName = "Mars Perseverance Rover",
                    NumberOfSatellites = 1,
                    Manufacturer = "NASA",
                    DestinationOrbit = "Jezero rater, Mars",
                    Description = "Perserverance will search for signs of ancient life on Mars, collect the first rock samples to be returned to Earth anddd demonstrate the ability to convert CO2 into oxygen on the Red Planet",
                    MissionID = 7,
                    Mission = m7,
                    Image="marsrover.jpg"
                };
                Payload ingenuity = new Payload()
                {
                    PayloadID = 11,
                    PayloadName = "Ingenuity Helicopter",
                    NumberOfSatellites = 1,
                    Manufacturer = "NASA",
                    DestinationOrbit = "Jezero Crater, Mars",
                    Description = "Ingenuity's mission is to perform the first ever powered flight on another world and to help plan driving routes for rovers.",
                    MissionID = 7,
                    Mission = m7,
                    Image = "ingenuity.jpg"
                };
                /*Payload Nusat8 = new Payload()
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
                mdb.Missions.Add(m7);
              //  mdb.Missions.Add(m7);

                //mdb.Rockets.Add(electron);

                mdb.Payloads.Add(marsrover);
                mdb.Payloads.Add(ingenuity);
              //  mdb.Payloads.Add(Nusat8);

                mdb.SaveChanges();
                Console.WriteLine("Saved to database");
            }

        }
    }
}
