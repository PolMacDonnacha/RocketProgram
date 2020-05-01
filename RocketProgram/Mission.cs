using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Timers;

namespace RocketProgram
{
    public class Mission
    {
        public int MissionID { get; set; }
        public string MissionName { get; set; }
        public string MissionDescription { get; set; }
        public DateTime LaunchDate { get; set; }
        public string LaunchSite { get; set; }

        public int RocketID { get; set; }
        public virtual List<Payload> Payloads { get; set; }
        public virtual Rocket Rocket { get; set; }

       
        public TimeSpan Countdown()
        {
            TimeSpan timeLeft = LaunchDate - DateTime.Now;
            return timeLeft;
        }
        public override string ToString()
        {
            return ($"{MissionName}");
        }
    }
}
