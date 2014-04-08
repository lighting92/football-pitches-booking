using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.RecommendationModels
{
    public class RecommendationPriorityModel
    {
        
        public Configuration BestStadiumsMostBooked { get; set; }
        public Configuration BestStadiumsNearest { get; set; }
        public Configuration BestStadiumsMostDiscount { get; set; }

        public Configuration NearestStadiumsMostBooked { get; set; }
        public Configuration NearestStadiumsNearest { get; set; }
        public Configuration NearestStadiumsMostDiscount { get; set; }

        public Configuration PromotionStadiumsMostBooked { get; set; }
        public Configuration PromotionStadiumsNearest { get; set; }
        public Configuration PromotionStadiumsMostDiscount { get; set; }

        public Configuration MostBookedStadiumsMostBooked { get; set; }
        public Configuration MostBookedStadiumsNearest { get; set; }
        public Configuration MostBookedStadiumsMostDiscount { get; set; }

        public Configuration RivalAtStadiumMostBooked { get; set; }
        public Configuration RivalAtStadiumNearest { get; set; }
        public Configuration RivalExpired { get; set; } 
    }
}