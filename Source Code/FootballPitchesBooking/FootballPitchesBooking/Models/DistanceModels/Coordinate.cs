using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.DistanceModels
{
    public class Coordinate
    {
        public double laditude { get; set; }
        public double longitude { get; set; }

        public Coordinate(double lad, double log)
        {
            this.laditude = lad;
            this.longitude = log;
        }
    }
}