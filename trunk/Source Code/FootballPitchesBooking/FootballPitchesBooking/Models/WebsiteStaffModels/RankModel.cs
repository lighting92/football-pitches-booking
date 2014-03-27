using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.WebsiteStaffModels
{
    public class RankModel
    {
        public int Id { get; set; }
        public string RankName { get; set; }
        public int Point { get; set; }
        public string Promotion { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}