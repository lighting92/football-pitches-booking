using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.AccountModels
{
    public class BookingHistoryModel
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public string StadiumName { get; set; }
        public string StadiumAddress { get; set; }
        public string Price { get; set; }
        public string CreateDate { get; set; }
        public string Status { get; set; }
        public string RivalStatus { get; set; }
    }
}