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

        public Configuration AppropriateStadiumsMostBooked { get; set; }
        public Configuration AppropriateStadiumsNearest { get; set; }
        public Configuration AppropriateStadiumsMostDiscount { get; set; }

        public Configuration PromotionStadiumsMostBooked { get; set; }
        public Configuration PromotionStadiumsNearest { get; set; }
        public Configuration PromotionStadiumsMostDiscount { get; set; }
    }
}