using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.RecommendationModels
{
    public class RecommendStadium
    {
        public Stadium Stadium { get; set; }
        public Promotion HighestPromotion { get; set; }
        public string RecommendationReason { get; set; }
        public double CorrespondingRate { get; set; }
        public int? Distance { get; set; }
        public int BookedCount { get; set; }
        
    }

}