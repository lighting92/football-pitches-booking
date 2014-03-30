using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.RecommendationModels
{
    public class RecommendationTypeModel
    {
        public int Id { get; set; }
        public int PriorityId {get;set;}
        public String PriorityType { get; set; }
        public float PriorityPercent { get; set; }
    }
}