using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.WebsiteStaffModels
{
    public class ReservationModel
    {
        public Reservation Resv { get; set; }
        public List<Field> Fields { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string StadiumName { get; set; }
        public string StadiumAddress { get; set; }
    }
}