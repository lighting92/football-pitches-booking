using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumModels
{
    public class StadiumModel
    {
        public Stadium Stadium { get; set; }
        public List<StadiumImage> Images { get; set; }
        public List<StadiumReview> Reviews { get; set; }
        public double Rate { get; set; }
        public int RateCount { get; set; }
    }
}