using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.RankModels
{
    public class RankModel
    {
        public string RankName { get; set; }
        public int Point { get; set; }
        public string Promotion { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}