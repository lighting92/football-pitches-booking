using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.RecommendationModels
{
    public class XMLUserDistance
    {
        public int UserId { get; set; }
        public string UserAddress { get; set; }
        public string UpdateDate { get; set; }
        public List<StadiumDistance> StadiumsDistance { get; set; }
    }
}