using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.RecommendationModels
{
    public class RecommendationModel
    {
        public List<RecommendStadium> Stadiums { get; set; }
        public List<StadiumImage> StadiumImages { get; set; }
        public List<RecommendStadium> PromotionStadiums {get;set;}
        public List<StadiumImage> PromotionStadiumImages { get; set; }
    }
}