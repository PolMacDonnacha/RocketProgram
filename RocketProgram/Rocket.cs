﻿using System;
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
        public string Image { get; set; }
        public string CountryOfOrigin { get; set; }


        public override string ToString()
        {
            return ($"Rocket Name\n{RocketName} \n\nManufacturer\n{Manufacturer} \n\nCountry of Origin:\t{CountryOfOrigin}");
        }
    }
    

    public class MissionData : DbContext
    {
        public MissionData() : base("MissionData") { }
        public DbSet<Rocket> Rockets { get; set; }
        public DbSet<Payload> Payloads { get; set; }
        public DbSet<Mission> Missions { get; set; }
    }
}
