using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumModels
{
    public class QuickSearchModel
    {
        public string FieldType { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public string District { get; set; }
        public string City { get; set; }

        public List<QuickSearchResultModel> Results { get; set; }
        public string SearchResultString { get; set; }
        public string ErrorMessage { get; set; }
    }
}