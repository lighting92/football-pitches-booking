using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Models.StadiumModels
{
    public class JoinUsModel
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string StadiumName { get; set; }
        public string StadiumAddress { get; set; }
        public string Note { get; set; }
        public string ErrorMessage;
    }
}