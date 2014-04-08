using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.RecommendationModels
{
    public class StadiumDistance
    {
        public int StadiumId { get; set; }
        public string StadiumAddress { get; set; }
        public int? Distance { get; set; }
    }
}