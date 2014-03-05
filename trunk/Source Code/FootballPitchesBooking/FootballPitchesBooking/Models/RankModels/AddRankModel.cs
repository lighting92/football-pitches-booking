using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.RankModels
{
    public class AddRankModel
    {
        public string RankName { get; set; }
        public int Point { get; set; }
        public string Promotion { get; set; }
        public string ErrorMessage { get; set; }
    }
}