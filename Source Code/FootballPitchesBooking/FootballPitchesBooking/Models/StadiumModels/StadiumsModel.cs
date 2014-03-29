using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumModels
{
    public class StadiumsModel
    {
        public List<Stadium> Stadiums { get; set; }
        public List<double> Rate { get; set; }
        public List<string> Image { get; set; }
    }
}