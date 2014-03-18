using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumModels
{
    public class QuickSearchResultModel
    {
        public Stadium Stadium { get; set; }
        public List<Field> Fields { get; set; }
        public List<double> Prices { get; set; }
    }
}