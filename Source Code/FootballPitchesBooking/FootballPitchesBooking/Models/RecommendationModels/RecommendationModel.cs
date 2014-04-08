using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.RecommendationModels
{
    public class RecommendationModel
    {
        public List<RecommendStadium> BestStadiums { get; set; }
        public List<RecommendStadium> NearestStadiums { get; set; }
        public List<RecommendStadium> PromotionStadiums { get; set; }
        public List<RecommendStadium> MostBookedStadiums { get; set; }

        public List<StadiumImage> BestStadiumsImages { get; set; }
        public List<StadiumImage> NearestStadiumsImages { get; set; }
        public List<StadiumImage> PromotionStadiumsImages { get; set; }
        public List<StadiumImage> MostBookedStadiumsImages { get; set; }

        
    }
}