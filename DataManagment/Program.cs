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
                Mission m3 = new Mission() 
                {   MissionID = 3,
                    MissionName = "Jilin 1, NuSat 7 & NuSat 8",
                    MissionDescription = "A Chinese Long March 2D launched a small satellite for the Jilin 1 Earth observation constellation owned by Chang Guang Satellite Technology Co. Ltd. The Long March 2D also launched the ÑuSat 7 and ÑuSat 8 Earth observation microsatellites for Satellogic, a company based on Argentina.",
                    LaunchDate = new DateTime(2020,01,15),
                    LaunchSite = "Taiyuan, China",
                    RocketID = 3
                };
                Rocket LongMarch2D = new Rocket()
                {
                    RocketID = 3,
                    RocketName = "Long March 2D",
                    Manufacturer = "China Academy of Launch Vehicle Technology",
                    Description = "The Long March 2D, also known as the Chang Zheng 2D, CZ-2D and LM-2D, is a Chinese orbital carrier rocket. It is a 2-stage carrier rocket mainly used for launching LEO and SSO satellites. It is most commonly used to launch FSW-2 and -3 reconnaissance satellites.",
                    Image = "longmarch2d.jpg",
                    CountryOfOrigin = "China",
                    MissionID = 3,
                    //Mission = m3
                };
                Payload Jilin1 = new Payload()
                {
                    PayloadID = 3,
                    PayloadName = "Jilin 1",
                    NumberOfSatellites = 1,
                    Manufacturer = "Chang Guang Satellite Technology Co.",
                    DestinationOrbit = "Sun Synchronous Orbit",
                    Description = "Jilin-1 Optical-A is an high-definition optical satellite with a 0.72 m resolution pan-chromatic camera and 4 m resolution multi-spectral camera.",
                    MissionID = 3,
                    Mission = m3
                };
                Payload Nusat7 = new Payload()
                {
                    PayloadID = 4,
                    PayloadName = "NuSat 7",
                    NumberOfSatellites = 1,
                    Manufacturer = "Satellogic",
                    DestinationOrbit = "Sun Synchronous Orbit",
                    Description = "NuSat satellites are equipped with an imaging system operating in visible light and infrared. The constellation will allow for commercially available real-time Earth imaging and video with a ground resolution of 3.3 ft (1 m).",
                    MissionID = 3,
                    Mission = m3
                };
                Payload Nusat8 = new Payload()
                {
                    PayloadID = 5,
                    PayloadName = "NuSat 8",
                    NumberOfSatellites = 1,
                    Manufacturer = "Satellogic",
                    DestinationOrbit = "Sun Synchronous Orbit",
                    Description = "NuSat satellites are equipped with an imaging system operating in visible light and infrared. The constellation will allow for commercially available real-time Earth imaging and video with a ground resolution of 3.3 ft (1 m).",
                    MissionID = 3,
                    Mission = m3
                };
                mdb.Missions.Add(m3);

                mdb.Rockets.Add(LongMarch2D);

                mdb.Payloads.Add(Jilin1);
                mdb.Payloads.Add(Nusat7);
                mdb.Payloads.Add(Nusat8);

                mdb.SaveChanges();
                Console.WriteLine("Saved to database");
            }

        }
    }
}
